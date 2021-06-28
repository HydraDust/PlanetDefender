using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Current High Score: " + PlayerPrefs.GetInt("HighScore").ToString("N0");
    }
}
