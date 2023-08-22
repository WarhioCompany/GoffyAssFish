using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class SHITSpikeScipt : MonoBehaviour
{
    public Vector3 spikeStartPos;
    public Vector3 spikeTipOffset;

    public float rotationSpeed;
    public float shootingSpeed;

    public float retrieveSpeed;
    public float retrieveRotationSpeed;

    private float _currentSpikeSpeed;
    private float _currentSpikeRotationSpeed;

    public float shootingRange;

    public float initialRotation;

    public SHITSpikeManager manager;

    public Sprite sprite;

    Vector3 target;
    Vector3 initialTipPosition;

    float targetLocalY;

    public enum SpikeState
    {
        None, 
        Preparing,
        Shoot,
        Retrieving
    }
    public SpikeState state;

    // Start is called before the first frame update
    void Start()
    {
        initialTipPosition = GetTipPosition();
        initialRotation = transform.localRotation.eulerAngles.z;
        transform.GetChild(0).transform.localPosition = spikeStartPos;
        targetLocalY = spikeStartPos.y;
        _currentSpikeSpeed = shootingSpeed;
        _currentSpikeRotationSpeed = rotationSpeed;
    }

    public Vector3 GetTipPosition()
    {
        return transform.TransformPoint(spikeTipOffset + transform.GetChild(0).localPosition);
    }
    public void Shoot(Vector3 mousePos)
    {
        state = SpikeState.Shoot;
        target = Vector3.Normalize(mousePos) * shootingRange * 5;
        targetLocalY = spikeStartPos.y + shootingRange;
        manager.state = SHITSpikeManager.SpikeManagerState.Flying;
    }
    void Retrieve()
    {
        _currentSpikeSpeed = retrieveSpeed;
        _currentSpikeRotationSpeed = retrieveRotationSpeed;
        target = initialTipPosition;
        targetLocalY = spikeStartPos.y;
        state = SpikeState.Retrieving;
        manager.state = SHITSpikeManager.SpikeManagerState.Retrieving;
    }
    // Update is called once per frame
    void Update()
    {
        if (manager.state == SHITSpikeManager.SpikeManagerState.Prepare && manager.closestSpike == this)
        {
            target = manager.getMousePos();
        }
        else if (state == SpikeState.Shoot)
        {

            if (Vector3.Distance(transform.position, GetTipPosition()) > shootingRange)
            {
                //Miss
                Retrieve();
            }
        }
        else 
        {
            target = initialTipPosition;
        }



        float rot = 0;
        if (target == initialTipPosition)
        {
            rot = initialRotation;
        }
        else
        {
            Vector2 diff = target - GetTipPosition();
            rot = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
        }
        if(Mathf.Abs(transform.GetChild(0).localPosition.y - targetLocalY) < 0.1 && state == SpikeState.Retrieving)
        {
            //Retrieved
            print("No fucking way");
            state = SpikeState.None;
            manager.state = SHITSpikeManager.SpikeManagerState.None;
            _currentSpikeSpeed = shootingSpeed;
            _currentSpikeRotationSpeed = rotationSpeed;
        }
        transform.rotation =  Quaternion.Euler(0, 0, Mathf.LerpAngle(transform.localRotation.eulerAngles.z, rot, Time.deltaTime * _currentSpikeRotationSpeed));
        transform.GetChild(0).localPosition = new Vector3(0, Mathf.Clamp(Mathf.Lerp(transform.GetChild(0).localPosition.y, targetLocalY, _currentSpikeSpeed * Time.deltaTime), spikeStartPos.y, shootingRange));
    }
}
