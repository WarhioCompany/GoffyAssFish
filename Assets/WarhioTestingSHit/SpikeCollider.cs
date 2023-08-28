using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeCollider : MonoBehaviour
{
    //private void OnTriggerStay(Collider collision)
    //{
    //    //print("onStay");
    //    if (collision.gameObject.tag == "Bubble")
    //    {
    //        return;
    //    }
    //    GetComponentInParent<SHITSpikeScipt>().Hit(collision);
    //}

    public float hitCooldown = 0;
    private float hitTimer;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            collision.GetComponent<Bubble>().Explode();
        }
        else
        {
            if (hitTimer > 0 && GetComponentInParent<SHITSpikeScipt>().state == SHITSpikeScipt.SpikeState.Shoot) return;
            hitTimer = hitCooldown;
            GetComponentInParent<SHITSpikeScipt>().Hit(collision);
        }
    }

    private void Update()
    {
        if (hitTimer > 0)
        {
            hitTimer -= Time.deltaTime;
        }
    }
}
