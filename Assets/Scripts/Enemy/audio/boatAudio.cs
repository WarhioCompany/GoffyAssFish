using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boatAudio : MonoBehaviour
{
    private AudioSource source;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void ShootHarpoon()
    {
        AudioClip sfx = SoundManager.instance.getClip("harpoon_shot");
        if (sfx != null)
            source.PlayOneShot(SoundManager.instance.getClip("harpoon_shot")); 
    }
}
