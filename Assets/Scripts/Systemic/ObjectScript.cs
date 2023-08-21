using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    public float disolveTime;
    public float mass;

    public bool isPlayerAttached;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerAttached)
        {
            disolveTime -= Time.deltaTime;
        }
    }
}
