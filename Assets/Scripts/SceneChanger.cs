using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
        
    }

    public void Arcade()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Arcade");
        
    }

    public void Survival()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Survival");
    }
}
