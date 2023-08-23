using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public GameObject dissolveParticles;
    public GameObject finalDissolvePrefab;
    public float disolveTime;
    public float mass;

    public bool isPlayerAttached;

    private Vector3 orgScale;
    private bool destroyed;

    [Header("Randomization Settings")]
    public float minScale;
    public float maxScale;

    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
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
            float dissolveSpeed = 10;

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
        float newScale = Random.Range(minScale, maxScale);
        retVal.x = newScale;
        retVal.y = newScale;

        // set mass

        orgScale = retVal;
        return retVal;
    }
}
