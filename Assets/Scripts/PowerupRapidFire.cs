using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupRapidFire : MonoBehaviour
{
    public float rateIncrease;
    public float duration = 6f;

    public void Activate()
    {
        StartCoroutine(StartPowerup());
    }

    IEnumerator StartPowerup()
    {
        float old = GetComponent<PlayerShooter>().fireRate;
        GetComponent<PlayerShooter>().fireRate = old + rateIncrease;
        yield return new WaitForSeconds(duration);
        GetComponent<PlayerShooter>().fireRate = old;
    }
}
