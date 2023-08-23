using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkHarpoon : MonoBehaviour
{
    public float speed;
    public float acceleration;

    private Rigidbody rb; // Changed to Rigidbody for 3D movement
    private bool hit;

    private void OnTriggerEnter(Collider other)
    {
        if (hit || other.CompareTag("Attack") || other.CompareTag("enemy")) return;

        // Stick to object, then destroy after 2 seconds
        transform.parent = other.transform;
        other.GetComponent<Rigidbody>().AddForceAtPosition(rb.velocity, other.ClosestPoint(rb.position));

        speed = 0;
        Destroy(rb);
        Destroy(gameObject, 5);

        hit = true;

        if (other.CompareTag("Player"))
        {
            // Hit the player
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hit) return;
        if (rb.velocity.magnitude <= speed)
        {
            rb.AddForce(transform.right * acceleration * Time.deltaTime);
        }
    }
}
