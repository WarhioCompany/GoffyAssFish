using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;

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

    public void setMasterVolume(float volume) {
        mixer.SetFloat("masterVolume", volume);
    }

    public void setSoundEffectsVolume(float volume)
    {
        mixer.SetFloat("soundFXVolume", volume);
    }

    #region KakashiOverload

    public void setMasterVolume(Slider _slider){
        mixer.SetFloat("masterVolume", _slider.value);
    }

    public void setMusicVolume(Slider _slider) {
        mixer.SetFloat("musicVolume", _slider.value);

    }

    public void setSoundEffectsVolume(Slider _slider){
        mixer.SetFloat("soundFXVolume", _slider.value);
    }

    #endregion
}
