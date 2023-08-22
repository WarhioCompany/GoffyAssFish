using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureMeter : MonoBehaviour
{
    public GameObject arrow;

    private float maxPressure = 117.33f;
    private float rotationSpeed = 10f;
    private float maxWiggleAngle = 5f;

    private void Update()
    {
        float percent = getCurBars() / maxPressure;
        float angle = 180 * percent;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        // Apply random wiggle rotation
        Quaternion wiggleRotation = Quaternion.Euler(0, 0, Random.Range(-maxWiggleAngle, maxWiggleAngle));

        // Combine the target rotation with the wiggle rotation
        Quaternion finalRotation = Quaternion.Slerp(arrow.transform.rotation, targetRotation * wiggleRotation, Time.deltaTime * rotationSpeed);

        // Rotate the arrow
        arrow.transform.rotation = finalRotation;
    }

    public float getCurBars()
    {
        return (GameValues.height * 997 * 9.807f) / 100000;
    }
}
