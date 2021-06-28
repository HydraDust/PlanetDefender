using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Health health;
    public TextMeshProUGUI healthText;

    void Update()
    {
        healthText.text = health.GetHealth().ToString();
    }
}
