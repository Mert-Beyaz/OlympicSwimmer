using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    public PlayerController player;
    public void StartSwimming()
    {
        player.isPlayerJump = true;
        this.gameObject.SetActive(false);
    }
    public void OnTryAgainClick()
    {
        SceneManager.LoadScene(0);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}

