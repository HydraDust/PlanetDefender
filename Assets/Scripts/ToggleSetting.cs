using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleSetting : MonoBehaviour, ISetting
{
    [SerializeField] public UnityEvent ToggleUpdate = new UnityEvent();

    public Toggle toggle;
    public string playerPrefVar;
    public bool defaultValue;

    void Awake()
    {
        if (!PlayerPrefs.HasKey(playerPrefVar))
        {
            PlayerPrefs.SetInt(playerPrefVar, defaultValue?1:0);
        }
    }

    void Start()
    {
        SetValue();
    }

    public void SetValue()
    {
        toggle.isOn = PlayerPrefs.GetInt(playerPrefVar, defaultValue?1:0) == 1;
    }

    public void ValueUpdate()
    {
        PlayerPrefs.SetInt(playerPrefVar, toggle.isOn?1:0);
        ToggleUpdate.Invoke();
    }
}
