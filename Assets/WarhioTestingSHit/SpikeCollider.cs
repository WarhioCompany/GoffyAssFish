using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollider : MonoBehaviour
{
    private void OnTriggerStay(Collider collision)
    {
        //print("onStay");
        GetComponentInParent<SHITSpikeScipt>().Hit(collision);
    }
    private void OnTriggerEnter(Collider collision)
    {
        //just in case
        GetComponentInParent<SHITSpikeScipt>().Hit(collision);
    }
}
