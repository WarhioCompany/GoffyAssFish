using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeCam : MonoBehaviour
{
    public static ScreenShakeCam Instance { get; private set; }

    float timerShake;
    float timerShakeTotal;
    float startingIntensity;
    private CinemachineVirtualCamera cam;

    private void Awake()
    {
        Instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (timerShake > 0)
        {
            timerShake -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            noise.m_AmplitudeGain = Mathf.Lerp(startingIntensity, 0, 1 - (timerShake / timerShakeTotal));
        }
    }

    public void ShakeCam(float intensity, float timer)
    {
        CinemachineBasicMultiChannelPerlin noise = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        noise.m_AmplitudeGain = intensity;

        startingIntensity = intensity;
        timerShakeTotal = timer;
        timerShake = timer;
    }
}