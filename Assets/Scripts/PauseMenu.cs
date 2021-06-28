using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject hudUI;
    public GameObject pauseUI;
    public GameObject blurVolume;

    public Button firstSelected;

    PlayerControls controls;

    public bool survive = false;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Pause.started += ctx => Pause();
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Pause()
    {
        if (pauseUI.activeInHierarchy)
        {
            hudUI.SetActive(true);
            pauseUI.SetActive(false);
            blurVolume.SetActive(false);
            Time.timeScale = 1;

            if (!survive)
                AudioManager.instance.Play("GameMusic", false);
            else
                AudioManager.instance.Play("SurviveMusic", false);
        }
        else
        {
            hudUI.SetActive(false);
            pauseUI.SetActive(true);
            blurVolume.SetActive(true);
            Time.timeScale = 0;
            firstSelected.Select();
            
            if (!survive)
                AudioManager.instance.Pause("GameMusic");
            else
                AudioManager.instance.Pause("SurviveMusic");
        }
    }
}
