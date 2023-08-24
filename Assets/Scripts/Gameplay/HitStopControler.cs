using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopControler : MonoBehaviour
{
    public static HitStopControler instance { get; private set; }

    private float speed;
    private bool restoreTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        restoreTime = false;
    }

    private void Update()
    {
        if (restoreTime)
        {
            if (Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime * speed;
            }
        }
    }

    public void StopTime(float changeTime, int RestoreSpeed, float delay)
    {
        speed = RestoreSpeed;

        if (delay > 0)
        {
            StopCoroutine(StartTimeAgain(delay));
            StartCoroutine(StartTimeAgain(delay));
        }
        else
        {
            restoreTime = true;
        }

        Time.timeScale = changeTime;
    }

    IEnumerator StartTimeAgain(float amt)
    {
        restoreTime = true;
        yield return new WaitForSeconds(amt);
    }
}
