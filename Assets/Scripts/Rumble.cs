using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rumble : MonoBehaviour
{
    public static Rumble instance;

    void Awake()
    {
        instance = this;
    }

    public void RumbleConstant(float low, float high, float duration)
    {
        if (Gamepad.current == null || PlayerPrefs.GetInt("Rumble", 1) == 0)
            return;
        
        StopCoroutine("StartRumble");
        StartCoroutine(StartRumble(low, high, duration));
    }

    IEnumerator StartRumble(float low, float high, float duration)
    {
        Gamepad.current.SetMotorSpeeds(low, high);
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
}
