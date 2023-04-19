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
        transform.DOLocalMove(new Vector3(player.transform.position.x, player.transform.position.y + 7, player.transform.position.z - 9), 0.1f);

        if (!PlayerController.Instance.isGameEnd)
            return;

        
        seq.Append(transform.DOLocalMove(new Vector3(1.46f, 2.26f, 3.11f), 2));
        seq.Join(transform.DOLocalRotate(new Vector3(17, -145, 1), 2));
    }
}