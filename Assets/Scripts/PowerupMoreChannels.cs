using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMoreChannels : MonoBehaviour
{
    public int channelIncrease;
    public float duration = 6f;

    public void Activate()
    {
        StartCoroutine(StartPowerup());
    }

    IEnumerator StartPowerup()
    {
        int old = GetComponent<PlayerShooter>().channels;
        GetComponent<PlayerShooter>().channels = old + channelIncrease;
        yield return new WaitForSeconds(duration);
        GetComponent<PlayerShooter>().channels = old;
    }
}
