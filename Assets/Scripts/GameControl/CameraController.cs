using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraController : MonoBehaviour
{
    Sequence seq;
    public PlayerController player;

    private void Start()
    {
        seq = DOTween.Sequence();
    }
    void Update()
    {
        if (player.RankList.Count >= 3)
        {
            StartCoroutine(WaitFinish());
            IEnumerator WaitFinish()
            {
                yield return new WaitForSeconds(3);
                seq.Append(transform.DOLocalMove(new Vector3(237.91f, 11.47f, 252.88f), 1));
                seq.Join(transform.DOLocalRotate(new Vector3(18, 45, 2), 2));
                ///burada sıkıntı var update diye
                player.audioSource.clip = player.Clips[4];
                player.audioSource.Play();
                player.audioSource.loop = true;

            }
        }

        else if (PlayerController.Instance.isGameEnd)
        {
            seq.Append(transform.DOLocalMove(new Vector3(32.5f, 12.3f, 168.6f), 0.5f));
            seq.Join(transform.DOLocalRotate(new Vector3(18, -78.6f, 0), 2));
        }

        else
        {
            transform.DOLocalMove(new Vector3(player.transform.position.x, player.transform.position.y + 7, player.transform.position.z - 9), 0.1f);
        }

    }
}
