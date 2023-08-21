using UnityEngine.Audio;
using UnityEngine;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    public AudioMixer mixer;

    public AudioClip musicClip;
    public AudioSource musicSource;

    private AudioSource source;
    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayMusic();
    }

    public void PlaySound(AudioClip sound)
    {
        source.PlayOneShot(sound);
    }
    public void PlayMusic()
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void setMusicVolume(float volume)
    {
        mixer.SetFloat("musicVolume", volume);
    }

    public void setSoundEffectsVolume(float volume)
    {
        mixer.SetFloat("soundFXVolume", volume);
    }
}
