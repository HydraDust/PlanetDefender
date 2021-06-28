using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighTime : MonoBehaviour
{
    void Start()
    {
        float bestTime = PlayerPrefs.GetFloat("BestTime");
        int minutes = (int) Mathf.Floor(bestTime/60);
        int seconds = (int) Mathf.Floor((bestTime/60 - minutes) * 60);

        GetComponent<TextMeshProUGUI>().text = "Current Longest Time: " + minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
