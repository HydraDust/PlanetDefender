using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkybox : MonoBehaviour
{
    public Material[] skyboxes;

    void Awake()
    {
        RenderSettings.skybox = skyboxes[Random.Range(0, skyboxes.Length)];
        DynamicGI.UpdateEnvironment();
    }
}
