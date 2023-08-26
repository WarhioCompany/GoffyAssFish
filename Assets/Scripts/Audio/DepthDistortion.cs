using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class DepthDistortion : MonoBehaviour
{
    public AudioMixer mixer;
    public float minEffectValue;
    public float maxEffectValue;

    public float maxBoostValue;

    private float _effectRatio;
    private float _boostRatio;

    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("Replace calculations with pre-calculated ratios");
        _effectRatio = (maxEffectValue - minEffectValue) / GameValues.MaxHeight;
        _boostRatio = maxBoostValue / GameValues.MaxHeight;
    }

    // Update is called once per frame
    void Update()
    {
        // berechne prozent von aktueller höhe zu maxhöhe

        // value = prozent * maxvalue + minvalue

        if (SceneManager.GetActiveScene().name == "Main")
        {
            float percent = (-GameValues.height * GameValues.heightMeterRatio) / GameValues.MaxHeight;
            float value = (1-percent*2) * maxEffectValue + minEffectValue;
            mixer.SetFloat("depthEffect", value);
        }

        //mixer.SetFloat("depthEffect", maxEffectValue - (maxEffectValue - minEffectValue) / GameValues.MaxHeight * -GameValues.height); 
        mixer.SetFloat("depthVolumeBoost", maxBoostValue / GameValues.MaxHeight * GameValues.height);
    }
}
