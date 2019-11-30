using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Text m_CoutDownTimerText;
    [HideInInspector] public float m_GameScore = 0.0f;
    public Text m_Score;


    public void CanvasScore()
    {
        m_Score.text = m_GameScore.ToString("F0");
    }

    private void Update()
    {
        if (GameController.Instance.m_gameStart && !GameController.Instance.m_GamePaused)
        {
            m_GameScore += Time.deltaTime;
            m_GameScore += 0.5f; //love hardcoding!
            CanvasScore();
        }
    }
}


