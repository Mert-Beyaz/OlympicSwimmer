using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ButtonController : MonoBehaviour
{
    public Image SelectionImage, ShopPanelImage;
    public GameObject Short;

    public void OnTryAgainClick()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShopPanel()
    {
        SelectionImage.gameObject.SetActive(false);
        ShopPanelImage.gameObject.SetActive(true);
        PlayerController.Instance.audioSource.clip = PlayerController.Instance.Clips[0];
        PlayerController.Instance.audioSource.Play();
    }

    public void QuitShop()
    {
        SelectionImage.gameObject.SetActive(true);
        ShopPanelImage.gameObject.SetActive(false);
    }
}

