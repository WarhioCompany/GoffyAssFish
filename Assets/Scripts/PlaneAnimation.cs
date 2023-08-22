using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneAnimation : MonoBehaviour
{
    public enum animationStates
    {
        STOP,
        PLAYING
    }

    public animationStates state;

    public float timeBetween;
    public float pictures;

    float counter = 0;
    Material mat;
    bool waiting;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
        StartCoroutine(wait());
    }

    private void Update()
    {
        if (state == animationStates.PLAYING && !waiting)
        {
            mat.mainTextureOffset = new Vector2 (1/pictures * counter, 0f);
            counter++;
            if (counter >= pictures)
            {
                counter = 0;
            }
            StartCoroutine(wait());
        }
    }

    public void Play()
    {
        state = animationStates.PLAYING;
    }
    public void Stop()
    {
        state = animationStates.STOP;
    }

    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(timeBetween);
        waiting = false;
    }
}
