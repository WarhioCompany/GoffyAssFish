using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHeightTracker : MonoBehaviour
{
    public float heightMeterRatio;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(new Vector3(0, -GameValues.MaxHeight / GameValues.heightMeterRatio, 0), 5f);
    }

    private void Start()
    {
        GameValues.heightMeterRatio = heightMeterRatio;
    }
    private void Update()
    {
        GameValues.height = Mathf.Clamp(transform.position.y / heightMeterRatio, -GameValues.MaxHeight, 0); 
    }
}
