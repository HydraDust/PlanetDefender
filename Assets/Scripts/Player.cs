using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{
    public GameObject deathVFX;

    CinemachineImpulseSource impulse;

    void Start()
    {
        impulse = FindObjectOfType<CinemachineImpulseSource>();

        GetComponent<Health>().OnDeath.AddListener(PlayerDeath);        
        GetComponent<Health>().OnDamageTaken.AddListener(PlayerDamaged);
    }

    void PlayerDeath()
    {
        if (deathVFX)
            Instantiate(deathVFX, transform.position, transform.rotation);

        if (PlayerPrefs.GetInt("ScreenShake", 1) == 1)
            impulse.GenerateImpulse(3.5f);
        
        AudioManager.instance.Play("PlayerDeath");

        FindObjectOfType<Score>().ResetMultiplier();

        if (PlayerPrefs.GetInt("Rumble", 1) == 1)
            Rumble.instance.RumbleConstant(0.75f, 0.25f, 0.4f);
    }

    void PlayerDamaged()
    {
        if (PlayerPrefs.GetInt("ScreenShake", 1) == 1)
            impulse.GenerateImpulse(2f);

        AudioManager.instance.Play("PlayerDamaged");

        FindObjectOfType<Score>().ResetMultiplier();

        if (PlayerPrefs.GetInt("Rumble", 1) == 1)
            Rumble.instance.RumbleConstant(0.75f, 0.25f, 0.4f);
    }
}
