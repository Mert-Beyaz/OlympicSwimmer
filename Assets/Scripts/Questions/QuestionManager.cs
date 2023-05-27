using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public int correctAnswerIndex;
    public GameObject correctPanel, wrongPanel, QuestionPanel;
    public Question[] questions;
    public TMP_Text questionText;
    public TMP_Text[] buttonTexts;

    private void Start()
    {
        SetQuestion();
    }

    public void AnswerButton (int answerIndex)
    {
        if (answerIndex == correctAnswerIndex)
        {
            correctPanel.gameObject.SetActive(true);
            StartCoroutine(LookAnswer());
            IEnumerator LookAnswer()
            {
                yield return new WaitForSeconds(2);
                correctPanel.gameObject.SetActive(false);
                QuestionPanel.gameObject.SetActive(false);
                PlayerController.Instance.isCorrectAnswer = true;
                PlayerController.Instance.Jump();
            }
            
        }
        else
        {
            wrongPanel.gameObject.SetActive(true);
            StartCoroutine(LookAnswer());
            IEnumerator LookAnswer()
            {
                yield return new WaitForSeconds(2);
                wrongPanel.gameObject.SetActive(false);
                QuestionPanel.gameObject.SetActive(false);
                PlayerController.Instance.isCorrectAnswer = false;
                PlayerController.Instance.Jump();
            }
        }
    }

    private void SetQuestion()
    {
        //random seç soruyu
        int currentQuestion = Random.Range(0, questions.Length);

        questionText.text = questions[currentQuestion].questionText;

        for (int i = 0; i < buttonTexts.Length; i++)
        {
            buttonTexts[i].text = questions[currentQuestion].answers[i];
            correctAnswerIndex = questions[currentQuestion].correctAnswerIndex;
        }
    }
}
