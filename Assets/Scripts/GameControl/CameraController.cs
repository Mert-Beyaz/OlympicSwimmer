using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CameraController : MonoBehaviour
{
    Sequence seq;
    public PlayerController player;
    bool Clapping, Waiting;
    

    private void Start()
    {
        seq = DOTween.Sequence();
    }
    void Update()
    {
        if (player.RankList.Count >= 3)
        {
            if (!Clapping)
            {
                StartCoroutine(WaitFinish());
                Clapping = true;
            }

            IEnumerator WaitFinish()
            {
                yield return new WaitForSeconds(3);
                seq.Append(transform.DOLocalMove(new Vector3(237.91f, 11.47f, 252.88f), 1));
                seq.Join(transform.DOLocalRotate(new Vector3(18, 45, 2), 2));
                player.PlayerText.gameObject.SetActive(true);
                player.Ranking.SetActive(false);
                player.Particular.SetActive(true);
                player.audioSource.clip = player.Clips[4];
                player.audioSource.Play();
                player.audioSource.loop = true;
                yield return new WaitForSeconds(5);
                player.FinishUIimage.gameObject.SetActive(true);
            }
        }

        else if (player.isGameEnd)
        {
            if (!Waiting)
            {
                Waiting = true;
                seq.Append(transform.DOLocalMove(new Vector3(32.5f, 12.3f, 101.07f), 0.5f));
                seq.Join(transform.DOLocalRotate(new Vector3(18, -70.2f, 0), 2));
                StartCoroutine(PlayAudio());
                IEnumerator PlayAudio()
                {
                    yield return new WaitForSeconds(4);
                    player.audioSource.clip = player.Clips[0];
                    player.audioSource.Play();
                    player.audioSource.loop = true;
                }
            }
        }

        else
        {
            transform.DOLocalMove(new Vector3(player.transform.position.x, player.transform.position.y + 7, player.transform.position.z - 9), 0.1f);
        }

    }
}