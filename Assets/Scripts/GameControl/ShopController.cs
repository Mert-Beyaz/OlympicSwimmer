using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public Image SelectionImage, ShopPanelImage;
    public GameObject Short;
    Color objectColor;
    

    private void Start()
    {
        objectColor = Short.GetComponent<SkinnedMeshRenderer>().material.color;  
    }


    public void SelectShortColor()
    {
        if (PlayerController.Instance.CoinCounter >= 10)
        {
            PlayerController.Instance.CoinCounter -= 10;
            PlayerPrefs.SetInt("Coin", PlayerController.Instance.CoinCounter);
            PlayerController.Instance.CoinCounterText.text = "Coin = " + PlayerController.Instance.CoinCounter;

            objectColor = this.gameObject.GetComponent<Image>().color;//değişti

            PlayerPrefs.SetFloat("red", objectColor.r);
            PlayerPrefs.SetFloat("green", objectColor.g);
            PlayerPrefs.SetFloat("blue", objectColor.b);
            PlayerPrefs.Save();

            SelectionImage.gameObject.SetActive(true);
            ShopPanelImage.gameObject.SetActive(false);
        }
    }
}
