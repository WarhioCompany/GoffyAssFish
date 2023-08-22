using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Movemnet of the Enemy 

    public enum enemyState
    {
        MOVE,
        ATTACK
    }

    public enemyState state;

    [Header("Movement")]
    public float acceleration;
    public float yAcceleration;
    public float xSpeed;
    public float ySpeed;
    public float boostMultiplier = 3;  
    public float maxXPosOffset;
    public float maxYDist; // gets boost when he is here + teleport 
    public float resetYDist; // boosts get resettet

    private GameObject player;
    private Rigidbody2D rb;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x - maxXPosOffset, transform.position.y, 0), new Vector3(transform.position.x - maxXPosOffset, transform.position.y - maxYDist, 0));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x + maxXPosOffset, transform.position.y, 0), new Vector3(transform.position.x + maxXPosOffset, transform.position.y - maxYDist, 0));
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // keep enemy over player, as long as he is not attacking. Stay still when Attacking
        if (state == enemyState.ATTACK)
        {
            return;
        }

        float distance = Vector2.Distance(player.transform.position, transform.position);

        // Follow Player X
        float xmin = player.transform.position.x - maxXPosOffset;
        float xmax = player.transform.position.x + maxXPosOffset;
        float acc = acceleration * distance;

        // move left/ Right depending on there the player is
        if (transform.position.x < xmin && rb.velocity.x < xSpeed)
        {
            // too far on the left
            Debug.Log("Moving Right");
            rb.AddForce(Vector3.right * acceleration * Time.deltaTime);

        }
        if (transform.position.x > xmax && rb.velocity.x < xSpeed)
        {
            // too far on the right
            Debug.Log("Moving Left");
            rb.AddForce(-1 * Vector3.right * acceleration * Time.deltaTime);
        }

        // Descend
        float finalYSpeed = ySpeed;
        if (distance > maxYDist)
        {
            // boost + teleport
            finalYSpeed *= boostMultiplier;
        }

        if (rb.velocity.y > -finalYSpeed)
        {
            // normally descend
            rb.AddForce(-1 * Vector3.up * yAcceleration * Time.deltaTime);
            rb.drag = 0.5f;
        }
        else
        {
            rb.drag = 6;
        }
    }


    public void PleaseDie()
    {
        // The enemy implodes and dies
        Destroy(gameObject);
    }
}
