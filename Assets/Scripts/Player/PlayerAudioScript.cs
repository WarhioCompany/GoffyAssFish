using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    public AudioSource source;

    public void ShootSpike()
    {
        source.PlayOneShot(SoundManager.instance.getClip("spike_shot"));
    }
    //public void HitSpike()
    //{
    //    source.PlayOneShot(SoundManager.instance.getClip("hit_spike"));
    //}
    public void Pull()
    {
        source.PlayOneShot(SoundManager.instance.getClip("spike_pull"));
    }
    public void Push()
    {
        source.PlayOneShot(SoundManager.instance.getClip("spike_push"));
    }
}
