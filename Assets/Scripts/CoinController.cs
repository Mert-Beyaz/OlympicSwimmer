using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public float speed;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Swimmer" || other.gameObject.tag == "SwimmerAi")
        {
            StartCoroutine(DestroyItem());
            IEnumerator DestroyItem()
            {
                gameObject.transform.DOScale(Vector3.zero, speed);
                yield return new WaitForSeconds(speed);
                Destroy(this.gameObject);
            }
        }
    }
}
