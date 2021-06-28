﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    void Start()
    {
        AudioManager.instance.Play("TitleMusic");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
