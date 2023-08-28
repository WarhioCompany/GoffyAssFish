using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienEnviromentUpdater : MonoBehaviour
{
    // changes:
    // 1. Alpha of water effect to hide sky
    public float maxAlpha;
    public float minAlpha;
    public Material waterMat;

    // 2. Skybox Lighting multiplier

    private void Update()
    {
        // clamp water between 0.5 and 1
        float set = GameValues.height * GameValues.heightMeterRatio / -GameValues.MaxHeight + 0.5f;
        waterMat.SetFloat("_alpha", set);

        // clamp light between 0 and 1.7
    }
}
