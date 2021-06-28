using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SliderSetting : MonoBehaviour, ISetting
{
    [SerializeField] public UnityEvent SliderUpdate = new UnityEvent();

    public string playerPrefVar;
    public Slider slider;
    public TextMeshProUGUI percentText;
    public int defaultValue;

    void Awake()
    {
        if (!PlayerPrefs.HasKey(playerPrefVar))
        {
            PlayerPrefs.SetInt(playerPrefVar, defaultValue);
        }
    }

    void Start()
    {
        SetValue();
    }

    public void SetValue()
    {
        slider.value = PlayerPrefs.GetInt(playerPrefVar, defaultValue);
    }

    public void ValueUpdate()
    {
        percentText.text = Mathf.Round(slider.value).ToString();
        PlayerPrefs.SetInt(playerPrefVar, (int) slider.value);
        SliderUpdate.Invoke();
    }
}
