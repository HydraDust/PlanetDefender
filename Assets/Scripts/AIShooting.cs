using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShooting : MonoBehaviour
{
    public float damage;
    public float speed;
    public int channels;
    public float channelAngle = 6f;
    public float fireRate;
    float nextTimeToFire;
    public float inaccuracy;

    public bool targeting = true;

    Transform target;


    void Start()
    {
        target = FindObjectOfType<GameManager>().player.transform;
        nextTimeToFire = (Random.value / fireRate);
    }

    void Update()
    {
        if ((!target || !target.gameObject.GetComponent<Health>().IsAlive()) && targeting)
            return;
        
        if (Time.time >= nextTimeToFire && Vector3.Distance(transform.position, target.position) < 6f)
        {
            Vector3 inputDir;
            if (targeting)
            {
                inputDir = transform.InverseTransformPoint(target.position);
                inputDir.y = 0;
            }
            else
            {
                inputDir = GetComponent<AIMovement>().GetDirection();
            }
            inputDir = inputDir.normalized;

            nextTimeToFire = Time.time + (1 / fireRate);

            float currentAngle = -(channels/2f * channelAngle);
            for (int i = 0; i < channels; i++)
            {
                float addedInaccuracy = Random.Range(-inaccuracy, inaccuracy);
                Vector3 shootAngle = (Quaternion.Euler(0, currentAngle + addedInaccuracy, 0) * inputDir).normalized;

                GameObject projGO = ObjectPooler.SharedInstance.GetPooledObject("Enemy Bullet"); 
                if (projGO != null) 
                {
                    projGO.transform.position = transform.position;
                    projGO.transform.rotation = transform.rotation;
                    projGO.SetActive(true);
                    projGO.GetComponent<Projectile>().Setup(shootAngle, damage, speed, 90f / speed);
                }

                currentAngle += channelAngle;
            }

            AudioManager.instance.Play("EnemyShoot");

        }
    }
}
