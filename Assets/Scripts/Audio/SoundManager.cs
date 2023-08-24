using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // SoundLibary
    public SoundLibary[] sounds;

    public AudioClip getClip(string identifyer)
    {
        foreach (var sound in sounds)
        {
            if (sound.identifyer == identifyer)
            {
                return sound.clip;
            }
        }

        return null;
    }
}

[Serializable]
public class SoundLibary
{
    public string identifyer;
    public AudioClip clip;
}
