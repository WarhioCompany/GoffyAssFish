using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpwardForce : MonoBehaviour
{
    public float acceleration;
    public float maxVelo;
    public float waterHeight;

    private SHITSpikeManager movement;
    private Rigidbody rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        movement = GetComponent<SHITSpikeManager>();
    }
    private void Update()
    {
        // Up if: Not attached and maxVel not reached;
        if (rigidBody.velocity.magnitude <= maxVelo && transform.position.y < waterHeight && movement.attachedSpike == null)
        {
            rigidBody.drag = 0;
            rigidBody.AddForce(Vector2.up * acceleration * Time.deltaTime);
        }
        else if (transform.position.y >= waterHeight)
        {
            rigidBody.drag = 1;
            rigidBody.AddForce(Vector2.down * acceleration * Time.deltaTime);
        }
        //else if (movement.attached)
        //{
        //    rigidBody.velocity = Vector3.zero;
        //}
    }
}
