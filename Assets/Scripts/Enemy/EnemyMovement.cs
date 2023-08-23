using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Movement of the Enemy

    public enum enemyState
    {
        MOVE,
        ATTACK
    }

    public bool active = true;
    public enemyState state;

    [Header("Movement")]
    public float acceleration;
    public float yAcceleration;
    public float xSpeed;
    public float ySpeed;
    public float boostMultiplier = 3;
    public float maxXPosOffset;
    public float maxYDist; // gets boost when he is here + teleport 
    public float resetYDist; // boosts get reset

    [Header("CC")]
    public float concussedTimer;

    private GameObject player;
    private Rigidbody rb; // Changed to Rigidbody for 3D movement

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x - maxXPosOffset, transform.position.y, transform.position.z), new Vector3(transform.position.x - maxXPosOffset, transform.position.y - maxYDist, transform.position.z));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x + maxXPosOffset, transform.position.y, transform.position.z), new Vector3(transform.position.x + maxXPosOffset, transform.position.y - maxYDist, transform.position.z));
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!active) return;

        if (concussedTimer > 0)
        {
            concussedTimer -= Time.deltaTime;
            return;
        }

        // Keep enemy over player, as long as he is not attacking. Stay still when attacking.
        if (state == enemyState.ATTACK)
        {
            return;
        }

        float distance = Vector3.Distance(player.transform.position, transform.position);

        // Follow Player X
        float xmin = player.transform.position.x - maxXPosOffset;
        float xmax = player.transform.position.x + maxXPosOffset;
        float acc = acceleration * distance;

        // Move left/right depending on where the player is.
        if (transform.position.x < xmin && rb.velocity.x < xSpeed)
        {
            // Too far on the left
            rb.AddForce(Vector3.right * acceleration * Time.deltaTime);
        }
        if (transform.position.x > xmax && rb.velocity.x > -xSpeed)
        {
            // Too far on the right
            rb.AddForce(-Vector3.right * acceleration * Time.deltaTime);
        }

        // Descend
        float finalYSpeed = ySpeed;
        if (distance > maxYDist)
        {
            // Boost + teleport
            finalYSpeed *= boostMultiplier;
        }

        if (rb.velocity.y > -finalYSpeed)
        {
            // Normally descend
            rb.AddForce(-Vector3.up * yAcceleration * Time.deltaTime);
            rb.drag = 0.5f;
        }
        else
        {
            rb.drag = 6;
        }
    }

    public void Concuss(float time)
    {
        // Add concussed behavior if needed
        concussedTimer = time;
    }

    public void PleaseDie()
    {
        // The enemy implodes and dies
        Destroy(gameObject);
    }
}
