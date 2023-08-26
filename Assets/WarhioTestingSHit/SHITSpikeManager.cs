using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class SHITSpikeManager : MonoBehaviour
{
    public GameObject spikePrefab;
    public int spikeAmount;
    public float attachRadius;
    public float radius;
    public float deadRange;
    public List<SHITSpikeScipt> spikeList;

    public float playerPullSpeed;

    public SpikeManagerState state = SpikeManagerState.None;

    public SHITSpikeScipt closestSpike;
    public SHITSpikeScipt attachedSpike;
    public GameObject lastObj;
    public float pushForce;

    public Camera cam;
    //public LayerMask mousePosLayer;
    //public GameObject follow;

    public bool canShoot;
 

    public enum SpikeManagerState
    {
        None,
        Prepare,
        Shoot,
        Flying,
        Retrieving
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(getMousePos(), 0.1f);
    }

    private void Awake()
    {
        canShoot = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnSpikes();
    }
    public Vector3 getMousePos()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 30.0f;
        screenPoint = cam.ScreenToWorldPoint(screenPoint);
        return screenPoint;

        //Vector3 pos = Vector3.zero;
        //Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    pos = hit.point;
        //    pos.z = 0;
        //}
        //return pos;

    }
    public int getClosestSpike()
    {
        float minDist = Mathf.Infinity;
        int spikeIndex = 0;
        for (int i = 0; i < spikeAmount; i++)
        {
            float dist = Vector3.Distance(spikeList[i].GetTipPosition(), getMousePos());
            if (dist < minDist && spikeList[i] != attachedSpike)
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
        if (!canShoot) return;
        Debug.Log("Shoot!");
        if (state == SpikeManagerState.None)
            state = SpikeManagerState.Prepare;
    }
    public void Shoot()
    {
        if (!canShoot) return;
        if (state == SpikeManagerState.Prepare)
        {
            state = SpikeManagerState.Shoot;
            GetComponent<Animator>().SetBool("grab", true);
        }
    }

    public void Push(Collider obj, SHITSpikeScipt spike)
    {
        Debug.Log("Pushing!");
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce((transform.position - spike.GetTipPosition()).normalized * pushForce, ForceMode.Impulse);
        GetComponent<Rigidbody>().AddForce(2f * pushForce * (spike.GetTipPosition() - transform.position).normalized, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.identity;
        //follow.transform.position = getMousePos();

        if (state == SpikeManagerState.Prepare)
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
                if (attachedSpike != null)
                {
                    attachedSpike.Detach();
                    attachedSpike = null;
                }
                spikeList[getClosestSpike()].Shoot(getMousePos(), transform.parent);
                transform.parent = null;
            }
            catch
            {
                print(getClosestSpike());
            }
            state = SpikeManagerState.Flying;
        }

        if (attachedSpike != null)
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
