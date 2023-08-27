using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    public AudioSource source;

    public void ShootSpike()
    {
        if (Time.timeScale > 0)
        {
            source.PlayOneShot(SoundManager.instance.getClip("spike_shot"));
        }
    }
    //public void HitSpike()
    //{
    //    source.PlayOneShot(SoundManager.instance.getClip("hit_spike"));
    //}
    public void Pull()
    {
        if (Time.timeScale > 0)
            source.PlayOneShot(SoundManager.instance.getClip("spike_pull"));
    }
    public void Push()
    {
        if (Time.timeScale > 0)
            source.PlayOneShot(SoundManager.instance.getClip("spike_push"));
    }
}
