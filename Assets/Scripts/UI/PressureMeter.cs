using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureMeter : MonoBehaviour
{
    public GameObject arrow;
    public float offset;
    public float totalMaxRotation;

    public float maxPressure = 117.33f;
    private float rotationSpeed = 10f;
    private float maxWiggleAngle = 5f;

    private void Start()
    {
        maxPressure = (GameValues.MaxHeight * 997f * 9.807f) / 100000;
    }

    private void Update()
    {
        float percent = getCurBars() / maxPressure;
        float angle = totalMaxRotation * percent;

        //Debug.Log(getCurBars() / maxPressure);
        //Debug.Log("Percent: " + getCurBars() + " / " + maxPressure + " = " + percent);
        //Debug.Log("Angle + Offset: " + (angle + offset).ToString());

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle+offset);

        // Apply random wiggle rotation
        Quaternion wiggleRotation = Quaternion.Euler(0, 0, Random.Range(-maxWiggleAngle, maxWiggleAngle));

        // Combine the target rotation with the wiggle rotation
        Quaternion finalRotation = Quaternion.Slerp(arrow.transform.rotation, targetRotation * wiggleRotation, Time.deltaTime * rotationSpeed);

        // Rotate the arrow
        arrow.transform.rotation = finalRotation;
    }

    public float getCurBars()
    {
        Debug.Log("Height: " + -GameValues.height);
        return (-GameValues.height * 997f * 9.807f) / 12000f;
    }
}
