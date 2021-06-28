using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class SliderSettingFloat : MonoBehaviour, ISetting
{
    [SerializeField] public UnityEvent SliderUpdate = new UnityEvent();

    public string playerPrefVar;
    public Slider slider;
    public TextMeshProUGUI percentText;
    public float defaultValue;

    void Awake()
    {
        if (!PlayerPrefs.HasKey(playerPrefVar))
        {
            PlayerPrefs.SetFloat(playerPrefVar, defaultValue);
        }
    }

    void Start()
    {
        SetValue();
    }

    public void SetValue()
    {
        slider.value = PlayerPrefs.GetFloat(playerPrefVar, defaultValue);
    }

    public void ValueUpdate()
    {
        percentText.text = (slider.value).ToString("0.00");
        PlayerPrefs.SetFloat(playerPrefVar, slider.value);
        SliderUpdate.Invoke();
    }
}
