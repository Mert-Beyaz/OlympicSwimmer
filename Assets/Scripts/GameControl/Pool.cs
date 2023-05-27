using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemsToCollectType
{
    Coin,
    Lifeline,
}

public class Pool : MonoBehaviour
{
    public GameObject CoinPrefab;
    public GameObject LifelinePrefab;
    public List<GameObject> ItemsToCollectPool = new List<GameObject>();

    private void Awake()
    {
        FillPool();
    }

    public void FillPool()
    {
        for (int i = 0; i < 100; i++)
        {
            var spawned = Instantiate(CoinPrefab, transform);
            spawned.SetActive(false);
            ItemsToCollectPool.Add(spawned);
        }

        for (int i = 0; i < 100; i++)
        {
            var spawned = Instantiate(LifelinePrefab, transform);
            spawned.SetActive(false);
            ItemsToCollectPool.Add(spawned);
        }
    }

    public GameObject GetItemsToCollectFromPool(ItemsToCollectType type)
    {
        ItemsToCollect Item = null;
        Item = GetItemsToCollect(type);
        if (Item == null)
        {
            FillPool();
            Item = GetItemsToCollect(type);
        }

        Item.transform.parent = null;
        Item.gameObject.SetActive(true);
        ItemsToCollectPool.Remove(Item.gameObject);
        return Item.gameObject;
    }

    private ItemsToCollect GetItemsToCollect(ItemsToCollectType type)
    {
        ItemsToCollect item = null;
        foreach (var i in ItemsToCollectPool)
        {
            if (i.GetComponent<ItemsToCollect>().ItemsToCollectType == type)
            {
                item = i.GetComponent<ItemsToCollect>();
                break;
            }
        }

        return item;
    }

    public void ResendItemToPool(GameObject Item)
    {
        Item.gameObject.SetActive(false);
        Item.transform.parent = transform;
        ItemsToCollectPool.Add(Item);
    }

}
