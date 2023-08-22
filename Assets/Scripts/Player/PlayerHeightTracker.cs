using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHeightTracker : MonoBehaviour
{
    public float heightMeterRatio;

    private void Start()
    {
        GameValues.heightMeterRatio = heightMeterRatio;
    }
    private void Update()
    {
        GameValues.height = Mathf.Clamp(transform.position.y * heightMeterRatio, -GameValues.MaxHeight, 0); 
    }
}
