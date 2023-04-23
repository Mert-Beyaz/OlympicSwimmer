using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public List<Transform> aiRoadTransformList = new List<Transform>();
    Transform aiTransform;
    public Animator AiAnimator;
    public float aiSpeed, force;
    bool isAiJump, AiSwimming;
    int leftRoad = 0;
    int rightRoad = 3;
    Sequence seq;
    public Pool pool;
    void Start()
    {
        aiTransform = GetComponent<Transform>();
        AiAnimator = GetComponent<Animator>();
        pool = GetComponent<Pool>();
    }

    void Update()
    {
        JumpAi();
        MoveAi();
    }

    void JumpAi()
    {
        //zıplama
        if (!PlayerController.Instance.isPlayerJump)
            return;

        if (!isAiJump)
        {
            AiAnimator.SetBool("Jump", true);//animator e jump i ekle dotween i ayarla
            aiTransform.DOMove(new Vector3(aiTransform.position.x, aiTransform.position.y - 4, aiTransform.position.z + 6), 2);
            StartCoroutine(WaitJump());
            IEnumerator WaitJump()
            {
                isAiJump = true;
                yield return new WaitForSeconds(2);
                AiSwimming = true;
            }
        }
    }
    void MoveAi()
    {
        if ((AiAnimator.GetBool("isFinishSwimming")))
            return;
        
        // yüzme
        if (AiSwimming)
        {
            AiSwimming = false;
            for (int i = 0; i < aiRoadTransformList.Count / 3; i++)
            {
                int nextRoad = Random.Range(leftRoad, rightRoad);

                aiTransform.position = Vector3.Lerp(aiTransform.position, aiRoadTransformList[nextRoad].position, aiSpeed);
                StartCoroutine(WaitMove());
                IEnumerator WaitMove()
                {
                    yield return new WaitForSeconds(aiSpeed);
                    leftRoad += 3;
                    rightRoad += 3;
                }
            }
        } 
    }

    void FinishRaceAi()
    {

    }




    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Lifeline")
        {
            StartCoroutine(DestroyItem());
            IEnumerator DestroyItem()
            {
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * force * aiSpeed);
                other.gameObject.transform.DOScale(Vector3.zero, aiSpeed / 2);
                yield return new WaitForSeconds(aiSpeed);
                pool.ResendItemToPool(other.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        //if (other.gameObject.tag == "Coin")
        //{
        //    StartCoroutine(DestroyItem());
        //    IEnumerator DestroyItem()
        //    {
        //        seq.Append(other.transform.DOLocalMoveY(1, aiSpeed / 2));
        //        seq.Join(other.transform.DOScale(Vector3.zero, aiSpeed / 2));
        //        yield return new WaitForSeconds(aiSpeed);
        //        pool.ResendItemToPool(other.gameObject);
        //    }
        //}

        if (other.gameObject.tag == "finishLine")
        {
            AiAnimator.SetBool("isFinishSwimming", true);
            StartCoroutine(EndGame());
            IEnumerator EndGame()
            {
                //playerTransform.position = Vector3.Lerp(playerTransform.position, (playerTransform.position + (new Vector3(1,1,1)), 0.5f * Time.deltaTime));
                aiTransform.DOMove(new Vector3(aiTransform.position.x, aiTransform.position.y - 0.5f, aiTransform.position.z + 2.5f), aiSpeed);
                yield return new WaitForSeconds(3);
                FinishRaceAi();
            }
        }
    }
}
