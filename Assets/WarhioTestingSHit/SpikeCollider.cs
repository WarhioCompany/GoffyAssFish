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
        if (collision.gameObject.tag == "Bubble")
        {
            collision.GetComponent<Bubble>().Explode();
        }
        else
        {
            GetComponentInParent<SHITSpikeScipt>().Hit(collision);

        }
        //just in case
    }
}
