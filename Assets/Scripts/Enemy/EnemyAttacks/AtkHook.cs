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
            other.transform.parent = null;
            other.GetComponent<SHITSpikeManager>().lastObj = null;
            other.GetComponent<SHITSpikeManager>().attachedSpike = null;
            other.GetComponent<SHITSpikeManager>().state = SHITSpikeManager.SpikeManagerState.None;
            other.GetComponent <Rigidbody>().AddForce(Vector3.up * 1.5f, ForceMode.Impulse);
        }
    }

    private void Start()
    {
        orgHeight = transform.position.y;
        StartCoroutine(wait());
    }

    private void Update()
    {
        float height = GameObject.FindGameObjectWithTag("Player").transform.position.y; // height in unity meters
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
