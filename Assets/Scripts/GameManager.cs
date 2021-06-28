using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] public UnityEvent OnGameOver = new UnityEvent();
    public GameObject player;

    public GameObject hudUI;
    public GameObject gameOverUI;
    public GameObject blurVolume;

    public Button firstSelected;

    void Start()
    {
        player.GetComponent<Health>().OnDeath.AddListener(GameOver);
    }

    void GameOver()
    {
        hudUI.SetActive(false);
        gameOverUI.SetActive(true);
        blurVolume.SetActive(true);

        firstSelected.Select();

        OnGameOver.Invoke();
    }

}
