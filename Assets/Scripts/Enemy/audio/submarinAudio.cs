using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submarinAudio : MonoBehaviour
{
    public float beepTime;

    public float beepTimer;
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (beepTimer > 0)
        {
            beepTimer -= Time.deltaTime;
        }
        else
        {
            beepTimer = beepTime;
            source.PlayOneShot(SoundManager.instance.getClip("sub_beep"));
        }
    }

    public void ShootHarpoon()
    {
        source.PlayOneShot(SoundManager.instance.getClip("harpoon_shot"));
    }
    public void ShootMissile()
    {
        source.PlayOneShot(SoundManager.instance.getClip("missile_shot"));
    }

}
