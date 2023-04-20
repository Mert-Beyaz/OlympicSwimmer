using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LifelineController : MonoBehaviour
{
    public float force;
    public float speed;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Swimmer" || other.gameObject.tag == "SwimmerAi")
        {
            StartCoroutine(DestroyItem());
            IEnumerator DestroyItem()
            {
                other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.back * force * speed);
                yield return new WaitForSeconds(speed);
                gameObject.transform.DOScale(Vector3.zero, speed);
                yield return new WaitForSeconds(speed);
                Destroy(this.gameObject);
            }
        }
    }
}
