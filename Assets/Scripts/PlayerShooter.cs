using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerShooter : MonoBehaviour
{
    public float damage;
    public float speed;
    public int channels;
    public float fireRate;
    float nextTimeToFire;
    public float inaccuracy;

    PlayerControls controls;

    Vector2 shootInput;

    CinemachineImpulseSource impulse;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Shoot.performed += ctx => shootInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Shoot.canceled += ctx => shootInput = Vector2.zero;

    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Start()
    {
        impulse = FindObjectOfType<CinemachineImpulseSource>();
    }

    void Update()
    {
        Vector3 inputDir = new Vector3(shootInput.x, 0, shootInput.y).normalized;

        if (channels > 0 && Time.time >= nextTimeToFire && shootInput.magnitude > 0.1f)
        {
            nextTimeToFire = Time.time + (1 / fireRate);

            float currentAngle = -(channels/2f * 6f);
            for (int i = 0; i < channels; i++)
            {
                float addedInaccuracy = Random.Range(-inaccuracy, inaccuracy);
                Vector3 shootAngle = (Quaternion.Euler(0, currentAngle + addedInaccuracy, 0) * inputDir).normalized;

                GameObject projGO = ObjectPooler.SharedInstance.GetPooledObject("Player Bullet"); 
                if (projGO != null) 
                {
                    projGO.transform.position = transform.position;
                    projGO.transform.rotation = transform.rotation;
                    projGO.SetActive(true);
                    projGO.GetComponent<Projectile>().Setup(shootAngle, damage, speed, 90f / speed);
                }

                currentAngle += 6;
            }

            if (PlayerPrefs.GetInt("ScreenShake", 1) == 1)
                impulse.GenerateImpulse(0.15f);

            AudioManager.instance.Play("PlayerShoot");
        }
    }
}
