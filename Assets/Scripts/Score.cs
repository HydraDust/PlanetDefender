using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public Slider decaySlider;

    int score = 0;

    public float multiplierDecayTime = 5f;
    float multiplier = 1f;
    float nextTimeToResetMulitplier;

    public bool survive = false;

    void Start()
    {
        if (survive)
            return;
        multiplierText.text = multiplier.ToString("F") + "x";
        decaySlider.maxValue = multiplierDecayTime;
    }

    void Update()
    {
        if (survive)
            return;
        
        float decay = nextTimeToResetMulitplier - Time.time;
        decaySlider.value = Mathf.Clamp(decay, 0f, 5f);

        if (multiplier > 1f && Time.time >= nextTimeToResetMulitplier)
        {
            ResetMultiplier();
        }
    }

    public void AddScore(int addScore)
    {
        if (survive)
            return;
        score += (int)Mathf.Round((float)addScore * multiplier);

        nextTimeToResetMulitplier = Time.time + multiplierDecayTime;
        multiplier += 0.05f;

        scoreText.text = score.ToString("N0");
        multiplierText.text = multiplier.ToString("F") + "x";
    }

    public void ResetMultiplier()
    {
        if (survive)
            return;
        multiplier = 1f;
        multiplierText.text = multiplier.ToString("F") + "x";
        nextTimeToResetMulitplier = Time.time;
    }

    public int GetScore()
    {
        return score;
    }
}
