using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnController : MonoBehaviour
{
    public Pool pool;
    public List<Transform> spawnTransformList = new List<Transform>();
    private int number;
    void Start()
    {
        SpawnItems();
    }

    public void SpawnItems()
    {
        int LifelineCounter = 0; //yan yana 3 tane cansimidi oluşmasın diye oluşturulmuş bir değişken
        foreach (var item in spawnTransformList)
        {
            GameObject Item = null;
            number = Random.Range(0, 2);
            if (LifelineCounter == 2)
            {
                number = 0;
                LifelineCounter = 0;
            }
            if (number == 0)
            {
                Item = pool.GetItemsToCollectFromPool(ItemsToCollectType.Coin);
            }
            else
            {
                Item = pool.GetItemsToCollectFromPool(ItemsToCollectType.Lifeline);
                LifelineCounter++;
            }
            Item.transform.position = item.position;
        }
    }
}

