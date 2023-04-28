using System.Collections;
using System.Collections.Generic;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class AiController : MonoBehaviour
{
    public List<Transform> aiRoadTransformList = new List<Transform>();
    Transform aiTransform;
    public Animator AiAnimator;
    float aiSpeed = 3f;
    public float force;
    bool isAiJump, AiSwimming;
    int leftRoad = 0;
    int rightRoad = 3;
    Sequence seq;
    public Pool pool;
    int nextRoad;

    void Start()
    {
        aiTransform = GetComponent<Transform>();
        AiAnimator = GetComponent<Animator>();
        pool = GetComponent<Pool>();
        nextRoad = Random.Range(0, 3);
    }

    void Update()
    {
        JumpAi();
        MoveAi();
        FinishRaceAi();
    }

    void JumpAi()
    {
        //zıplama
        if (!PlayerController.Instance.isPlayerJump)
            return;

        if (!isAiJump)
        {
            AiAnimator.SetBool("Jump", true);//animator e jump i ekle dotween i ayarla
            seq = DOTween.Sequence();
            seq.Append(aiTransform.DOMoveY(aiTransform.position.y + 1, 0.5f));
            seq.Append(aiTransform.DOMove(new Vector3(aiTransform.position.x, aiTransform.position.y - 5, aiTransform.position.z + 3), 2));
            seq.Append(aiTransform.DOMoveY(aiTransform.position.y - 4, 2));
            StartCoroutine(WaitJump());
            IEnumerator WaitJump()
            {
                isAiJump = true;
                yield return new WaitForSeconds(4.5f);
                AiSwimming = true;
            }
        }
    }
    void MoveAi()
    {
        if ((AiAnimator.GetBool("FinishRaceWin")))
            return;

        // yüzme

        if (AiSwimming)
        {
            AiAnimator.SetBool("Swim", true);
            aiTransform.position = Vector3.MoveTowards(aiTransform.position, aiRoadTransformList[nextRoad].position, aiSpeed * Time.deltaTime);
            if (aiTransform.position == aiRoadTransformList[nextRoad].position)
            {
                leftRoad += 3;
                rightRoad += 3;
                nextRoad = Random.Range(leftRoad, rightRoad);
                if (rightRoad > aiRoadTransformList.Count)
                {
                    AiSwimming = false;
                }
            }
        }
    }


    void FinishRaceAi()
    {
        if (PlayerController.Instance.isGameEnd || PlayerController.Instance.aiFinish)
        {
            
        }
        AiAnimator.SetBool("FinishRace", true);
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
        if (other.gameObject.tag == "finishLine")
        {
            AiSwimming = false;
            AiAnimator.SetBool("FinishRaceWin", true);
            PlayerController.Instance.aiFinish = true;
            PlayerController.Instance.RankList.Add(gameObject);
            aiTransform.DOMove(new Vector3(aiTransform.position.x, aiTransform.position.y - 0.4f, aiTransform.position.z + 3.5f), aiSpeed);
            PlayerController.Instance.isGameEnd = true;
        }
    }
}
