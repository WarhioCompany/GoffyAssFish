using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollider : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        print("onStay");
        GetComponentInParent<SHITSpikeScipt>().Hit(collision);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //just in case
        GetComponentInParent<SHITSpikeScipt>().Hit(collision);
    }
}
