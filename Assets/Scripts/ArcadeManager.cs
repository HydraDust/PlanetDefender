using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class ArcadeManager : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    void Start()
    {
        GetComponent<GameManager>().OnGameOver.AddListener(GameOver);
        AudioManager.instance.Play("GameMusic");
    }

    void GameOver()
    {

        int finalScore = GetComponent<Score>().GetScore();
        finalScoreText.text = finalScore.ToString("N0");

        if (PlayerPrefs.GetInt("HighScore") < finalScore)
        {
            PlayerPrefs.SetInt("HighScore", finalScore);
        }
    }
}
