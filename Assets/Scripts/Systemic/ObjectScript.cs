using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public GameObject dissolveParticles;
    public GameObject finalDissolvePrefab;
    public float disolveTime;
    public float mass;
    public float gravity;
    public float baseMass = 1;

    public bool isPlayerAttached;

    private Vector3 orgScale;
    private bool destroyed;
    private Rigidbody rb;

    [Header("Randomization Settings")]
    public float minScale;
    public float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        // movement
        if (rb.velocity.magnitude <= mass * gravity)
        {
            rb.AddForce(Vector3.down * mass * gravity * Time.deltaTime);
        }

        if (isPlayerAttached)
        {
            disolveTime -= Time.deltaTime;
            dissolveParticles.SetActive(true);
        }
        else
        {
            dissolveParticles.SetActive(false);
        }

        if(disolveTime <= 0)
        {
            //Destroy object (with animation)?
            float dissolveSpeed = 5;

            if (!destroyed)
            {
                Instantiate(finalDissolvePrefab, transform);
                destroyed = true;
            }

            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, dissolveSpeed * Time.deltaTime);
        }

        if (transform.position.y > (GameValues.height/GameValues.heightMeterRatio) + GameValues.maxObjHeightOffset 
            || transform.position.y < (GameValues.height / GameValues.heightMeterRatio) - GameValues.maxObjHeightOffset)

        {
            //Debug.Log("Deleted Object: " + gameObject.name);
            Debug.Log((GameValues.height / GameValues.heightMeterRatio) + GameValues.maxObjHeightOffset);

            Destroy(gameObject);
        }
    }

    public void Randomize()
    {
        transform.localScale = getRndScale();
    }

    public Vector3 getRndScale()
    {
        Vector3 retVal = Vector3.one;

        // randomize Scale
        float newScale = Random.Range(minScale * 100, maxScale * 100) / 100;
        retVal.x = newScale;
        retVal.y = newScale;

        // set mass
        mass = baseMass * newScale;
        GetComponent<Rigidbody>().mass = mass;

        orgScale = retVal;
        return retVal;
    }
}
