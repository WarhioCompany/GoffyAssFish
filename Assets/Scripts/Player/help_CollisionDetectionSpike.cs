using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class help_CollisionDetectionSpike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided: " + collision.tag);
        GetComponentInParent<PlayerTentacle>().Collided(collision);
    }
}
