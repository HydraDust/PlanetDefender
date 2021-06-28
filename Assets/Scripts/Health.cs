using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] public UnityEvent OnDeath = new UnityEvent();
    [SerializeField] public UnityEvent OnDamageTaken = new UnityEvent();

    public bool destroyOnDeath;

    bool alive = true;

    public float maxHealth;
    float currentHealth;

    public float hitInvincibleTime;
    bool invincible;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public float GetHealth()
    {
        return currentHealth;
    }

    public bool IsAlive()
    {
        return alive;
    }

    public void Damage(float damage)
    {
        if (!alive || invincible)
            return;
        
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            currentHealth = 0;
            alive = false;
            OnDeath.Invoke();
            
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            OnDamageTaken.Invoke();
            StartCoroutine(HitInvincibility());
            SetInvincible(hitInvincibleTime);
        }
    }

    public void Heal(float heal)
    {
        if (!alive)
            return;
        
        currentHealth += heal;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetInvincible(float duration)
    {
        if (!invincible)
            StartCoroutine(Invincibility(duration));
    }

    IEnumerator HitInvincibility()
    {
        MeshRenderer model = GetComponentInChildren<MeshRenderer>();
        float invincibilityDeltaTime = hitInvincibleTime / 20;

        for (float i = 0; i < hitInvincibleTime; i += invincibilityDeltaTime)
        {
            if (model.enabled == true)
            {
                model.enabled = false;
            }
            else
            {
                model.enabled = true;
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }
        model.enabled = true;
    }

    IEnumerator Invincibility(float duration)
    {
        invincible = true;
        yield return new WaitForSeconds(duration);
        invincible = false;
    }
}
