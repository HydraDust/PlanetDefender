using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        UpdateVolume();
    }

    public void Play(string name, bool fromStart = true)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        if (s.type == Sound.SoundType.Music)
        {
            foreach (Sound os in sounds)
            {
                if (os.type == Sound.SoundType.Music && os.source.isPlaying)
                {
                    os.source.Stop();
                }
            }
            if (fromStart)
            {
                s.source.time = 0f;
            }
        }
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;

        s.source.Stop();
    }

    public void UpdateVolume()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * ((float)PlayerPrefs.GetInt("MasterVolume", 80)/100);
            if (s.type == Sound.SoundType.SFX) s.source.volume *= (float)PlayerPrefs.GetInt("SFXVolume", 80)/100;
            else if (s.type == Sound.SoundType.Music) s.source.volume *= (float)PlayerPrefs.GetInt("MusicVolume", 80)/100;
            else if (s.type == Sound.SoundType.UI) s.source.volume *= (float)PlayerPrefs.GetInt("UIVolume", 80)/100;
        }
    }
}
