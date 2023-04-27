using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public PlayerController player;
 public void StartSwimming()
    {
        player.isPlayerJump = true;
        this.gameObject.SetActive(false);
    }
}
