using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SurvivalManager : MonoBehaviour
{
    public TextMeshProUGUI finalSecondsText;
    public TextMeshProUGUI finalMinutesText;

    public TextMeshProUGUI secondsText;
    public TextMeshProUGUI minutesText;

    float currentTime;

    void Start()
    {
        GetComponent<GameManager>().OnGameOver.AddListener(GameOver);
        AudioManager.instance.Play("SurviveMusic");
    }

    void GameOver()
    {
        float finalTime = currentTime;

        int minutes = (int) Mathf.Floor(finalTime/60);
        int seconds = (int) Mathf.Floor((finalTime/60 - minutes) * 60);

        finalMinutesText.text = minutes.ToString("00");
        finalSecondsText.text = seconds.ToString("00");

        if (PlayerPrefs.GetFloat("BestTime") < finalTime)
        {
            PlayerPrefs.SetFloat("BestTime", finalTime);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        int minutes = (int) Mathf.Floor(currentTime/60);
        int seconds = (int) Mathf.Floor((currentTime/60 - minutes) * 60);

        minutesText.text = minutes.ToString("00");
        secondsText.text = seconds.ToString("00");
    }
}
