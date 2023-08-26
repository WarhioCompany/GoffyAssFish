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
        SoundManager.instance.playOneShot("explosion");

        // Explode Animation (ParticleSystem)
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        // Get all objects in radius
        Collider[] objs = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider obj in objs)
        {
            if (obj.CompareTag("Attack")) continue;
            // push Objects away abit
            obj.GetComponent<Rigidbody>().AddForce((obj.transform.position - transform.position) * explosionStrenght, ForceMode.Impulse);

            if (obj.CompareTag("Object"))
            {
                Quaternion rotation = Quaternion.LookRotation(obj.transform.position - transform.position);
                Instantiate(dissolveParticle, obj.transform.position, rotation);
                Destroy(obj.gameObject, 2);
            }
        }
        Destroy(gameObject);
    }
}
