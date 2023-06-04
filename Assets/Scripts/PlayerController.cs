using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public Animator Animator;
    public Transform playerTransform, First, Second, Third;
    public Rigidbody rb;
    public Pool pool;
    public GameObject Ranking, Particular, Short;
    public float speed, force;
    public bool isGameEnd, isPlayerJump, aiFinish, isCorrectAnswer; //false
    Sequence seq;
    public int CoinCounter;
    public TMP_Text CoinCounterText, PlayerText;
    public Image WaitImage, ShopImage, FinishUIimage;
    public List<GameObject> RankList; //oyun tekrar başlayınca temizle
    public AudioSource audioSource;
    public AudioClip[] Clips;
    Color objectColor;

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
        RankList.Clear();
        WaitImage.gameObject.SetActive(false);
        audioSource.clip = Clips[0];
        audioSource.Play();
        LoadShortColor();

        objectColor = Short.GetComponent<SkinnedMeshRenderer>().material.color;

        if (PlayerPrefs.HasKey("Coin")) 
        {
            CoinCounter = PlayerPrefs.GetInt("Coin");
            CoinCounterText.text = "Coin = " + CoinCounter;
        }
    }


    void Update()
    {
        Move();
        FinishRace();
    }

    public void Jump()
    {
        if (isPlayerJump)
            return;
        
        if (isCorrectAnswer)
        {
            Ranking.gameObject.SetActive(true);
            Animator.SetBool("greatJump", true);
            playerTransform.DOMove(new Vector3(playerTransform.position.x, playerTransform.position.y - 4, playerTransform.position.z + 6), 2);
            StartCoroutine(WaitJump());
            IEnumerator WaitJump()
            {
                isPlayerJump = true;
                yield return new WaitForSeconds(0.5f);
                audioSource.clip = Clips[1];
                audioSource.Play();
                yield return new WaitForSeconds(1);
                audioSource.clip = Clips[2];
                audioSource.Play();
                yield return new WaitForSeconds(0.5f);
            }
            Animator.SetBool("isTreading", true);
        }

        else if (!isCorrectAnswer)
        {
            Ranking.gameObject.SetActive(true);
            Animator.SetBool("badJump", true);
            seq = DOTween.Sequence();
            seq.Append(playerTransform.DOMoveY(playerTransform.position.y + 2, 0.5f));
            seq.Append(playerTransform.DOMove(new Vector3(playerTransform.position.x, playerTransform.position.y - 5, playerTransform.position.z + 3), 2));
            seq.Append(playerTransform.DOMoveY(playerTransform.position.y - 4, 2));
            StartCoroutine(WaitJump());
            IEnumerator WaitJump()
            {
                isPlayerJump = true;
                yield return new WaitForSeconds(1f);
                audioSource.clip = Clips[1];
                audioSource.Play();
                yield return new WaitForSeconds(1);
                audioSource.clip = Clips[2];
                audioSource.Play();
                yield return new WaitForSeconds(2.5f);

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

        if (isGameEnd)
            return;
             
        if (Input.GetKey(KeyCode.W))
        {
            rb.velocity = new Vector3(0, 0, speed);
            playerTransform.DOMoveY(-2f, 1);
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
                playerTransform.DOMove(new Vector3(playerTransform.position.x - 2, playerTransform.position.y, playerTransform.position.z), (speed/20f));
            }
        }

        if (Input.GetKeyUp(KeyCode.D) && isPlayerJump && (playerTransform.position.x == -2 || playerTransform.position.x == 0))
        {
            if (playerTransform.position.x <= 2)
            {
                playerTransform.DOMove(new Vector3(playerTransform.position.x + 2, playerTransform.position.y, playerTransform.position.z), (speed/20f));
            }
        }
    }

    void FinishRace()
    {
        if (RankList.Count == 3)
        {
            isGameEnd = true;
            StartCoroutine(WaitFinish());
            IEnumerator WaitFinish()
            {
                yield return new WaitForSeconds(3);
                WaitImage.gameObject.SetActive(false);
                RankList[0].transform.position = First.position;
                RankList[0].transform.rotation = First.rotation;
                RankList[1].transform.position = Second.position;
                RankList[1].transform.rotation = Second.rotation;
                RankList[2].transform.position = Third.position;
                RankList[2].transform.rotation = Third.rotation;

                for (int i = 0; i < 3; i++)
                {
                    if (RankList[i].CompareTag("Player"))
                    {
                        Animator.SetBool("isWin", true);
                    }
                    else
                    {
                        Animator anim = RankList[i].gameObject.GetComponent<Animator>();
                        anim.SetBool("Win", true);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("finishLine"))
        {
            Animator.SetBool("isSwimming", false);
            Animator.SetBool("isTreading", false);
            Animator.SetBool("isFinishSwimming", true);
            playerTransform.DOMove(new Vector3(playerTransform.position.x, playerTransform.position.y - 1.2f, playerTransform.position.z + 3.7f), speed);
            isGameEnd = true;
            WaitImage.gameObject.SetActive(true);
            RankList.Add(gameObject);

            audioSource.clip = Clips[3];
            audioSource.Play();
            audioSource.loop = false;

            PlayerPrefs.SetInt("Coin", CoinCounter);
        }

        if (other.gameObject.CompareTag("Coin"))
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
        if (other.gameObject.CompareTag("Lifeline"))
        {
            StartCoroutine(DestroyItem());
            IEnumerator DestroyItem()
            {
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * force * speed);
                other.gameObject.transform.DOScale(Vector3.zero, speed / 2);
                yield return new WaitForSeconds(speed);
                other.gameObject.SetActive(false);
                pool.ResendItemToPool(other.gameObject);
            }
        }
    }

    private void LoadShortColor()
    {
        if (PlayerPrefs.HasKey("red") && PlayerPrefs.HasKey("green") && PlayerPrefs.HasKey("blue"))
        {
            float red = PlayerPrefs.GetFloat("red");
            float green = PlayerPrefs.GetFloat("green");
            float blue = PlayerPrefs.GetFloat("blue");
            Color color = new Color(red, green, blue);
            Short.GetComponent<SkinnedMeshRenderer>().material.color = color;
        }
    }
}
