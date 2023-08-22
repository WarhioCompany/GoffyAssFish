using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBomb : MonoBehaviour
{
    // spawn and move towards player in big to small curves

    public float timerTime;

    [Header("Explosion")]
    public GameObject explosionParticle;
    public GameObject dissolveParticle;
    public float explosionRadius;
    public float explosionStrenght;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(timerTime);

        // explode
        ScreenShakeCam.Instance.ShakeCam(10, 1);

        // Explode Animation (ParticleSystem)
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        // Get all objects in radius
        Collider[] objs = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider obj in objs)
        {
            if (obj.CompareTag("Attack")) continue;
            // push Objects away abit
            obj.GetComponent<Rigidbody>().AddForce((obj.transform.position - transform.position) * explosionStrenght, ForceMode.Impulse);

            // Calculate rotation to make the particle face away from the explosion
            Quaternion rotation = Quaternion.LookRotation(obj.transform.position - transform.position);

            // spawn ParticleBurst in direction away from explosion
            Instantiate(dissolveParticle, obj.transform.position, rotation);

            // destroy Prop
            Destroy(obj, 2);
        }
        Destroy(gameObject);
    }
}
