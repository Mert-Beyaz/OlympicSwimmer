using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public TMP_Text P1Text, P2Text, P3Text, P4Text, P5Text;
    public List<GameObject> PlayersList = new List<GameObject>();
    public GameObject FinishLine;
    public GameObject Ranking;

    void Update()
    {
        CalculateDistance();
    }

    void CalculateDistance()
    {
        if (PlayerController.Instance.isPlayerJump)
        {
            Ranking.gameObject.SetActive(true);
            PlayersList = PlayersList.OrderBy(obj => Vector3.Distance(obj.transform.position, FinishLine.transform.position)).ToList();
            P1Text.text = "1 - " + PlayersList[0].tag;
            P2Text.text = "2 - " + PlayersList[1].tag;
            P3Text.text = "3 - " + PlayersList[2].tag;
            P4Text.text = "4 - " + PlayersList[3].tag;
            P5Text.text = "5 - " + PlayersList[4].tag;

        }
    }
}
