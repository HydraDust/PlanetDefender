using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageTakenEffect : MonoBehaviour
{
    [Range(0.0f, 1.0f)] public float maxAlpha;
    float currentAlpha;

    public float fadeSpeed = 1f;
    public float fadeDelay = 0.1f;
    float fadeTime = 0;

    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        currentAlpha = 0;
    }
    
    void Update()
    {
        fadeTime += Time.deltaTime;

        if (fadeTime > fadeDelay)
        {
            currentAlpha = Mathf.Lerp(currentAlpha, 0, Time.deltaTime * fadeSpeed);
        }
        image.color = new Color(image.color.r, image.color.g, image.color.b, currentAlpha);
    }

    public void ShowEffect()
    {
        currentAlpha = maxAlpha;
        fadeTime = 0f;
    }
}
