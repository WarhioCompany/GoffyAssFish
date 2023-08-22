using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkHook : MonoBehaviour
{
    // Flow: It gets spawned, it descendes to the playerheight, it waits a bit, it goes up again

    public float descendSpeed;
    public float waitingTime;

    private bool waiting = true;
    private float orgHeight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player on Hook --> pull him up shortly
            Debug.Log("HitByHook");
        }
    }

    private void Start()
    {
        orgHeight = transform.position.y;
        StartCoroutine(wait());
    }

    private void Update()
    {
        float height = GameValues.height / GameValues.heightMeterRatio; // height in unity meters
        Vector3 targetPos;

        if (waiting)
        {
            targetPos = new Vector3(transform.position.x, height, 0);
        }
        else
        {
            targetPos = new Vector3(transform.position.x, orgHeight, 0);
        }
       
        transform.position = Vector3.Lerp(transform.position, targetPos, descendSpeed * Time.deltaTime);
    }

    IEnumerator wait()
    {
        waiting = true;
        yield return new WaitForSeconds(waitingTime);
        waiting = false;
        yield return new WaitForSeconds(waitingTime);
        Destroy(gameObject);
    }
}
