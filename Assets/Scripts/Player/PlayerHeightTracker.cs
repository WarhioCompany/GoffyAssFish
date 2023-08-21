using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHeightTracker : MonoBehaviour
{
    public float heightMeterRatio;
    private void Update()
    {
        GameValues.height = transform.position.y * heightMeterRatio; 
    }
}
