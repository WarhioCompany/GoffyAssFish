using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SHITSpikeManager : MonoBehaviour
{
    public GameObject spikePrefab;
    public int spikeAmount;
    public float radius;
    public float deadRange;
    public List<SHITSpikeScipt> spikeList;

    public SpikeManagerState state = SpikeManagerState.None;

    public SHITSpikeScipt closestSpike;

 

    public enum SpikeManagerState
    {
        None,
        Prepare,
        Shoot,
        Flying,
        Retrieving
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnSpikes();
    }
    public Vector3 getMousePos()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10.0f; 
        return Camera.main.ScreenToWorldPoint(screenPoint);
    }
    public int getClosestSpike()
    {
        float minDist = Mathf.Infinity;
        int spikeIndex = 0;
        for (int i = 0; i < spikeAmount; i++)
        {
            float dist = Vector3.Distance(spikeList[i].GetTipPosition(), getMousePos());
            if (dist < minDist)
            {
                spikeIndex = i;
                minDist = dist; 
            }
        }
        return spikeIndex;
    }

    void SpawnSpikes()
    {
        for (int i = 0; i < spikeAmount; i++)
        {
            float angle = i * (360.0f / spikeAmount) + 90;
            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 position = transform.position + new Vector3(x, y, 0);
            GameObject newObject = Instantiate(spikePrefab, position, Quaternion.identity);

            // Calculate the rotation angle
            float rotationAngle = angle - 90;

            // Apply the rotation
            newObject.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
            newObject.transform.parent = transform;

            newObject.GetComponent<SHITSpikeScipt>().manager = this;
            spikeList.Add(newObject.GetComponent<SHITSpikeScipt>());
        }
    }
    public void Prepare()
    {
        if (state == SpikeManagerState.None)
            state = SpikeManagerState.Prepare;
    }
    public void Shoot()
    {
        if (state == SpikeManagerState.Prepare)
            state = SpikeManagerState.Shoot;
    }
    // Update is called once per frame
    void Update()
    {
        if(state == SpikeManagerState.Prepare)
        {
            if (Vector3.Distance(getMousePos(), transform.position) < deadRange)
            {
                closestSpike = null;
            }
            else
            {
                closestSpike = spikeList[getClosestSpike()];
            }
        }
        else if (state == SpikeManagerState.Shoot && closestSpike != null)
        {
            try
            {
                spikeList[getClosestSpike()].Shoot(getMousePos());
            }
            catch
            {
                print(getClosestSpike());
            }
            state = SpikeManagerState.Flying;
        }
    }
}
