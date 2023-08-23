using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpwardForce : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float force;
    public float maxVelo;
    private PlayerMovement movement;
    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        movement = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        //if (transform.position.y < 0)
            rigidBody.AddForce(Vector2.up * force * Time.deltaTime);
        if (!(movement.playerState == PlayerMovement.PLAYERSTATE.ATTACHED))
        {
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxVelo);
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }   
    }
}
