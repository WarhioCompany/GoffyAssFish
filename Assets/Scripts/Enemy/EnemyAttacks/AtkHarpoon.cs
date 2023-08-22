using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkHarpoon : MonoBehaviour
{
    // shooting dir = Vector3.right
    // it spawns at the weapon, it moves forward

    public float speed;
    public float acceleration;

    private Rigidbody2D rb;
    private bool hit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hit) return;
        // stick to Object, then destroy after 2sec
        transform.parent = collision.transform;
        collision.GetComponent<Rigidbody2D>().AddForceAtPosition(rb.velocity, collision.ClosestPoint(rb.position));

        speed = 0;
        Destroy(rb);
        Destroy(gameObject, 5);

        hit = true;

        if (collision.CompareTag("Player"))
        {
            // hitted the player
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (hit) return;
        if (rb.velocity.x <= speed)
        {
            rb.AddForce(transform.right * acceleration * Time.deltaTime);
        }
    }
}
