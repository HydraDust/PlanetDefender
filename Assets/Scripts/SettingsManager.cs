using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public void UpdateAllSettings()
    {
        ISetting[] settings = GetComponentsInChildren<ISetting>();

        foreach(ISetting s in settings)
        {
            s.SetValue();
        }
    }
}
