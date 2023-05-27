using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnController : MonoBehaviour
{
    public Pool pool;
    public List<Transform> spawnTransformList = new List<Transform>();
    private int number;

    //ai ların hepsi için ayrı ayrı transform listesi oluşturmak lazım son satırları sil yerine transform listelerini koy

    void Start()
    {
        SpawnItems();
        Ai1SpawnItems();
        Ai2SpawnItems();
        Ai3SpawnItems();
        Ai4SpawnItems();
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

    void Ai1SpawnItems()
    {
        int LifelineCounter = 0; 
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
                continue;
            }
            else
            {
                Item = pool.GetItemsToCollectFromPool(ItemsToCollectType.Lifeline);
                LifelineCounter++;
            }
            Vector3 vec = item.position + new Vector3(8, 0, 0);
            Item.transform.position = vec;
        }
    }  
    
    void Ai2SpawnItems()
    {
        int LifelineCounter = 0;
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
                continue;
            }
            else
            {
                Item = pool.GetItemsToCollectFromPool(ItemsToCollectType.Lifeline);
                LifelineCounter++;
            }
            Vector3 vec = item.position + new Vector3(16, 0, 0);
            Item.transform.position = vec;
        }
    } 
    
    void Ai3SpawnItems()
    {
        int LifelineCounter = 0;
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
                continue;
            }
            else
            {
                Item = pool.GetItemsToCollectFromPool(ItemsToCollectType.Lifeline);
                LifelineCounter++;
            }
            Vector3 vec = item.position + new Vector3(-8, 0, 0);
            Item.transform.position = vec;
        }
    }
    
    void Ai4SpawnItems()
    {
        int LifelineCounter = 0;
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
                continue;
            }
            else
            {
                Item = pool.GetItemsToCollectFromPool(ItemsToCollectType.Lifeline);
                LifelineCounter++;
            }
            Vector3 vec = item.position + new Vector3(-16, 0, 0);
            Item.transform.position = vec;
        }
    }
}

