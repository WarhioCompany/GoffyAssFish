using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkMissile : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 2f;
    public float booster = 2;
    public float curveStrength = 0.2f;

    private Rigidbody rb;

    public GameObject explosionParticle;
    public GameObject dissolveParticle;
    public float explosionRadius;
    public float explosionStrength;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy") || other.CompareTag("Attack")) return;

        ScreenShakeCam.Instance.ShakeCam(15, 1.5f);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        SoundManager.instance.playOneShot("explosion");

        if (other.CompareTag("Player"))
        {
            other.transform.parent = null;
            other.GetComponent<SHITSpikeManager>().lastObj = null;
            other.GetComponent<SHITSpikeManager>().attachedSpike = null;
        }

        Collider[] objs = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider obj in objs)
        {
            if (obj.CompareTag("Attack") || obj.CompareTag("Player")) continue;

            Rigidbody objRb = obj.GetComponent<Rigidbody>();

            if (objRb != null)
            {
                objRb.AddExplosionForce(explosionStrength, transform.position, explosionRadius);
            }


            if (obj.CompareTag("Object"))
            {
                if (obj.GetComponentInChildren<SHITSpikeManager>())
                {
                    obj.GetComponentInChildren<SHITSpikeManager>().transform.parent = null;
                }
                Quaternion rotation = Quaternion.LookRotation(obj.transform.position - transform.position);
                Instantiate(dissolveParticle, obj.transform.position, rotation);
                Destroy(obj.gameObject, 2);
            }
        }

        Destroy(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        float distance = Vector3.Distance(rb.position, player.position);
        rotationSpeed = Mathf.Clamp(1 / distance * booster, 5, 100);

        // Calculate direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;

        // Calculate the angle to rotate towards the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Gradually rotate the missile towards the player with limited speed
        float step = rotationSpeed * Time.deltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
        rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRotation, step);

        // Move the missile forward
        rb.velocity = transform.right * moveSpeed;

        // Apply a force to curve the missile's path
        Vector3 curveForce = -transform.up * curveStrength;
        rb.AddForce(curveForce, ForceMode.Force);
    }
}
