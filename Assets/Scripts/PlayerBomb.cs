using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class PlayerBomb : MonoBehaviour
{
    public int bombs;
    int bombsLeft;

    public float speed;

    public TextMeshProUGUI bombCounter;

    PlayerControls controls;

    CinemachineImpulseSource impulse;

    bool deathBomb = false;

    public bool controlEnabled = true;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Bomb.started += ctx => Bomb();

    }

    void OnEnable()
    {
        if (controlEnabled)
            controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Start()
    {
        bombsLeft = bombs;
        if (bombCounter)
            bombCounter.text = bombsLeft.ToString();
        impulse = FindObjectOfType<CinemachineImpulseSource>();

        GetComponent<Health>().OnDeath.AddListener(DeathBomb);
    }

    void Bomb()
    {
        if (bombsLeft <= 0)
            return;

        float currentAngle = 0;
        for (int i = 0; i < 90; i++)
        {
            Vector3 shootAngle = (Quaternion.Euler(0, currentAngle, 0) * new Vector3(0, 0, 1f)).normalized;

            GameObject projGO = deathBomb ? ObjectPooler.SharedInstance.GetPooledObject("Bomb Proj Death") : ObjectPooler.SharedInstance.GetPooledObject("Bomb Proj"); 
            if (projGO != null) 
            {
                projGO.transform.position = transform.position;
                projGO.transform.rotation = transform.rotation;
                projGO.SetActive(true);
                projGO.GetComponent<Projectile>().Setup(shootAngle, 9999, speed, 180 / speed);
            }

            currentAngle += 4;
        }

        bombsLeft -= 1;
        if (bombCounter)
            bombCounter.text = bombsLeft.ToString();

        if (PlayerPrefs.GetInt("ScreenShake", 1) == 1)
            impulse.GenerateImpulse(2f);

        AudioManager.instance.Play("Bomb");

        FindObjectOfType<Score>().ResetMultiplier();
    }

    void DeathBomb()
    {
        bombsLeft += 1;
        deathBomb = true;
        Bomb();
    }
}
