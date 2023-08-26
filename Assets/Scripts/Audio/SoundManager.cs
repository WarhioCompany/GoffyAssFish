using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { private set; get; }

    // SoundLibary
    public SoundLibary[] sounds;

    private void Start()
    {
        instance = this;
    }

    public AudioClip getClip(string identifier)
    {
        AudioClip clip = null;

        foreach (var sound in sounds)
        {
            if (sound.identifyer == identifier)
            {
                clip = sound.clip;
                break;
            }
        }

        return clip;
    }

    public void playOneShot(string sound)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(getClip(sound));
    }
}

[Serializable]
public class SoundLibary
{
    public string identifyer;
    public AudioClip clip;
}
