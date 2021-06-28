using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform body;

    public Animator animator;

    public Slider slider;

    public float speed;

    public float boostSpeed;
    public float boostDuration;
    public float boostCooldown;
    bool boosting;
    bool boostOnCooldown;

    float currentSpeed;

    PlayerControls controls;

    Vector2 moveInput;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => moveInput = Vector2.zero;

        controls.Gameplay.Boost.started += ctx => Boost();

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
        currentSpeed = speed;
        slider.value = 1f;
        slider.fillRect.gameObject.GetComponent<Image>().color = new Color(0.16f, 0.8f, 0.16f, 0.9f);
    }

    void Update()
    {
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);

        if (direction.magnitude >= 0.1f)
        {
            transform.RotateAround(Vector3.zero, -transform.forward, currentSpeed * direction.x * Time.deltaTime);
            transform.RotateAround(Vector3.zero, transform.right, currentSpeed * direction.z * Time.deltaTime);

            Vector3 focus = body.position + (transform.right * direction.x) + (transform.forward * direction.z);
            Vector3 relativePos = focus - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos, body.up);
            body.rotation = Quaternion.Lerp(body.rotation, toRotation, 20f * Time.deltaTime);
        }
    }

    void Boost()
    {
        if (!boosting && !boostOnCooldown)
        {
            StartCoroutine(StartBoost());
            GetComponent<Health>().SetInvincible(boostDuration);
        }
    }

    IEnumerator StartBoost()
    {
        boosting = true;
        currentSpeed = boostSpeed;
        animator.SetBool("Boost", true);
        GetComponent<PlayerShooter>().enabled = false;
        
        float elasped = 0f;
        while (elasped < boostDuration)
        {
            slider.value = 1 - (elasped / boostDuration);

            yield return null;
            elasped += Time.deltaTime;
        }
        slider.value = 0f;
        slider.fillRect.gameObject.GetComponent<Image>().color = new Color(0.8f, 0.16f, 0.16f, 0.9f);

        GetComponent<PlayerShooter>().enabled = true;
        animator.SetBool("Boost", false);
        boosting = false;
        currentSpeed = speed;
        StartCoroutine(StartBoostCooldown());
    }

    IEnumerator StartBoostCooldown()
    {
        boostOnCooldown = true;

        float elasped = 0f;
        while (elasped < boostCooldown)
        {
            slider.value = elasped / boostCooldown;

            yield return null;
            elasped += Time.deltaTime;
        }
        slider.value = 1f;
        slider.fillRect.gameObject.GetComponent<Image>().color = new Color(0.16f, 0.8f, 0.16f, 0.9f);

        boostOnCooldown = false;

        AudioManager.instance.Play("BoostReady");
    }

    void OnTriggerEnter(Collider hit)
    {
        if (hit.CompareTag("Enemy"))
        {
            GetComponent<Health>().Damage(1);
        }
        
    }
}
