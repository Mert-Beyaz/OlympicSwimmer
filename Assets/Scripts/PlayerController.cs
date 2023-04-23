using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Animator Animator;
    public Transform playerTransform;
    public Rigidbody rb;
    public Pool pool;
    public float speed, force;
    public bool isGameEnd, isPlayerJump; //false
    Sequence seq;
    public int CoinCounter;
    public TMP_Text CoinCounterText;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    void Start()
    {
        Animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        pool = GetComponent<Pool>();
    }


    void Update()
    {
        Jump();
        Move();
    }

    private void Jump()
    {
        if (isPlayerJump)
            return;
        
        if (Input.GetKey(KeyCode.Space))
        {
            Animator.SetBool("greatJump", true);
            playerTransform.DOMove(new Vector3(playerTransform.position.x, playerTransform.position.y - 4, playerTransform.position.z + 6), 2);
            StartCoroutine(WaitJump());
            IEnumerator WaitJump()
            {
                isPlayerJump = true;
                yield return new WaitForSeconds(2);
            }
            Animator.SetBool("isTreading", true);
        }

        else if (Input.GetKey(KeyCode.LeftShift))
        {
            Animator.SetBool("badJump", true);
            seq = DOTween.Sequence();
            seq.Append(playerTransform.DOMoveY(playerTransform.position.y + 1, 0.5f));
            seq.Append(playerTransform.DOMove(new Vector3(playerTransform.position.x, playerTransform.position.y - 5, playerTransform.position.z + 3), 2));
            seq.Append(playerTransform.DOMoveY(playerTransform.position.y - 4, 2));
            StartCoroutine(WaitJump());
            IEnumerator WaitJump()
            {
                isPlayerJump = true;
                yield return new WaitForSeconds(4.5f);
            }
            Animator.SetBool("isTreading", true);
        }
    }

    private void Move()
    {
        if ((Animator.GetBool("isFinishSwimming")))
            return;

        if (!isPlayerJump)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0, 0, speed);
            playerTransform.DOMoveY(-1.8f, 1);
            Animator.SetBool("isSwimming", true);
            Animator.SetBool("isTreading", false);
        }

        else
        {
            playerTransform.DOMoveY(-3.22f, 1);
            Animator.SetBool("isSwimming", false);
            Animator.SetBool("isTreading", true);
        }

        if (Input.GetKeyUp(KeyCode.A) && isPlayerJump && (playerTransform.position.x == 2 || playerTransform.position.x == 0))
        {
            if (playerTransform.position.x >= -2)
            {
                playerTransform.DOMove(new Vector3(playerTransform.position.x - 2, playerTransform.position.y, playerTransform.position.z), (speed/5f));
                //Lerp ile yapmayı dene
            }
        }

        if (Input.GetKeyUp(KeyCode.D) && isPlayerJump && (playerTransform.position.x == -2 || playerTransform.position.x == 0))
        {
            if (playerTransform.position.x <= 2)
            {
                playerTransform.DOMove(new Vector3(playerTransform.position.x + 2, playerTransform.position.y, playerTransform.position.z), (speed/5f));
                //playerTransform.position = Vector3.Lerp(playerTransform.position, (playerTransform.position + (new Vector3(2, 0, 0))), speed * Time.deltaTime);           
                //Lerp ile yapmayı dene daha soft bir geçiş lazım
            }
        }
    }

    void FinishRace()
    {
        isGameEnd = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "finishLine")
        {
            Animator.SetBool("isSwimming", false);
            Animator.SetBool("isTreading", false);
            Animator.SetBool("isFinishSwimming", true);
            StartCoroutine(EndGame());
            IEnumerator EndGame()
            {
                //playerTransform.position = Vector3.Lerp(playerTransform.position, (playerTransform.position + (new Vector3(1,1,1)), 0.5f * Time.deltaTime));
                playerTransform.DOMove(new Vector3(playerTransform.position.x, playerTransform.position.y - 0.5f, playerTransform.position.z + 2.5f),speed);
                yield return new WaitForSeconds(3);
                FinishRace();
            }
        }

        if (other.gameObject.tag == "Coin")
        {
            CoinCounter++;
            CoinCounterText.text = "Coin = " + CoinCounter;
            StartCoroutine(DestroyItem());
            IEnumerator DestroyItem()
            {
                seq.Append(other.transform.DOLocalMoveY(1, speed / 2));
                seq.Join(other.transform.DOScale(Vector3.zero, speed / 2));
                yield return new WaitForSeconds(speed);
                pool.ResendItemToPool(other.gameObject);
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Lifeline")
        {
            StartCoroutine(DestroyItem());
            IEnumerator DestroyItem()
            {
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * force * speed);
                other.gameObject.transform.DOScale(Vector3.zero, speed / 2);
                yield return new WaitForSeconds(speed);
                pool.ResendItemToPool(other.gameObject);
            }
        }
    }
}
