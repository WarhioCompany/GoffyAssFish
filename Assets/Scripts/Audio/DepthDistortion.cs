using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class DepthDistortion : MonoBehaviour
{
    public AudioMixer mixer;
    public float minEffectValue;
    public float maxEffectValue;

    public float maxBoostValue;

    private float _effectRatio;
    private float _boostRatio;

    public float TESTHEIGHT;

    // Start is called before the first frame update
    void Start()
    {
        _effectRatio = (maxEffectValue - minEffectValue) / GameValues.MaxHeight;
        _boostRatio = maxBoostValue / GameValues.MaxHeight;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(maxEffectValue - (maxEffectValue - minEffectValue) / GameValues.MaxHeight * TESTHEIGHT);
        mixer.SetFloat("depthEffect", maxEffectValue - (maxEffectValue - minEffectValue) / GameValues.MaxHeight * TESTHEIGHT); //GameValues.height);
        mixer.SetFloat("depthVolumeBoost", maxBoostValue / GameValues.MaxHeight * TESTHEIGHT);
    }
}
