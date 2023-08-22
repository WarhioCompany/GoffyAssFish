using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AtkBomb : MonoBehaviour
{
    // spawn and move towards player in big to small curves

    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public float booster = 2;
    public float curveStrength = 0.2f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(rb.position, player.position);
        rotationSpeed = Mathf.Clamp(1 / distance * booster, 5, 15);

        // Calculate direction towards the player
        Vector2 direction = (player.position - transform.position).normalized;

        // Calculate the angle to rotate towards the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Rotate the missile towards the player with limited speed
        float step = rotationSpeed * Time.deltaTime;
        float newAngle = Mathf.MoveTowardsAngle(rb.rotation, angle, step);
        rb.rotation = newAngle;

        // Move the missile forward
        rb.velocity = transform.right * moveSpeed;

        // Apply a force to curve the missile's path
        Vector2 curveForce = -transform.up * curveStrength;
        rb.AddForce(curveForce, ForceMode2D.Force);
    }
}
