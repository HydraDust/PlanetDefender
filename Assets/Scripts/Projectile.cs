using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 direction;
    float damage;
    float speed;
    float endTime;

    public bool enemyTarget;

    public bool piercing;

    public GameObject hitVFX;

    public void Setup(Vector3 direction, float damage, float speed, float lifetime)
    {
        this.direction = direction.normalized;
        this.damage = damage;
        this.speed = speed;

        endTime = Time.time + lifetime;
    }

    void Update()
    {
        if (direction != null)
        {
            transform.RotateAround(Vector3.zero, -transform.forward, speed * direction.x * Time.deltaTime);
            transform.RotateAround(Vector3.zero, transform.right, speed * direction.z * Time.deltaTime);
        }
        if (Time.time >= endTime)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider hit)
    {
        Health h = hit.GetComponent<Health>();
        if (h && ((hit.CompareTag("Player") && enemyTarget == false) || (hit.CompareTag("Enemy") && enemyTarget == true)))
        {
            h.Damage(damage);

            // Rigidbody rb = hit.GetComponent<Rigidbody>();
            // if (rb)
            // {
            //     rb.AddForceAtPosition((hit.transform.position - transform.position).normalized * knockback, hit.ClosestPoint(transform.position));
            // }

            if (hitVFX)
            {
                Instantiate(hitVFX, transform.position, transform.rotation);
            }
            
            if (!piercing)
            {
                gameObject.SetActive(false);
            }
        }
        
    }
}
