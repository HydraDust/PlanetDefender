using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Enemy : MonoBehaviour
{
    public static List<GameObject> allEnemies = new List<GameObject>();

    public GameObject deathVFX;

    public int scoreValue;

    CinemachineImpulseSource impulse;

    void OnEnable()
    {
        allEnemies.Add(gameObject);
    }

    void OnDisable()
    {
        allEnemies.Remove(gameObject);
    }

    void Start()
    {
        impulse = FindObjectOfType<CinemachineImpulseSource>();

        GetComponent<Health>().OnDeath.AddListener(EnemyDeath);        
        GetComponent<Health>().OnDamageTaken.AddListener(EnemyDamaged);
    }

    void EnemyDeath()
    {
        if (deathVFX)
            Instantiate(deathVFX, transform.position, transform.rotation);

        if (PlayerPrefs.GetInt("ScreenShake", 1) == 1)
            impulse.GenerateImpulse(1.75f);

        AudioManager.instance.Play("EnemyDeath");

        FindObjectOfType<ShockwaveController>().Shockwave(transform.position);

        FindObjectOfType<Score>().AddScore(scoreValue);

        Collider[] nearEnemies = Physics.OverlapSphere(transform.position, 0.7f, (1 << 8));
        foreach (var enemy in nearEnemies)
        {
            enemy.GetComponent<Health>().Damage(50f);
        }
    
    }

    void EnemyDamaged()
    {
        if (PlayerPrefs.GetInt("ScreenShake", 1) == 1)
            impulse.GenerateImpulse(0.4f);

        AudioManager.instance.Play("EnemyDamaged");
    }
}
