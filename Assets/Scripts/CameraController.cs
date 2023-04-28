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
        if (!PlayerController.Instance.isGameEnd)
        {
            transform.DOLocalMove(new Vector3(player.transform.position.x, player.transform.position.y + 7, player.transform.position.z - 9), 0.1f);
        }
        else
        {
            seq.Append(transform.DOLocalMove(new Vector3(237.91f, 11.47f, 252.88f), 1));
            seq.Join(transform.DOLocalRotate(new Vector3(18, 45, 2), 2));
        }
        
    }
}