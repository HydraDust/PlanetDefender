using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    public enum SoundType
    {
        SFX, Music, UI
    }

    public string name;

    public SoundType type;

    public AudioClip clip;

    [Range(0f, 1f)] public float volume = 0.5f;
    [Range(0.1f, 3f)] public float pitch = 1f;

    public bool loop;

    [HideInInspector] public AudioSource source;

}
