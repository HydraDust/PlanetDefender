using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeUpdater : MonoBehaviour
{
    public void UpdateVolume()
    {
        AudioManager.instance.UpdateVolume();
    }
}
