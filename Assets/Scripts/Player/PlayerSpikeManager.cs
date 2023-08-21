using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSpikeManager : MonoBehaviour
{
    public int spikeCount;
    public float radius;
    public GameObject spikePrefab;

    public List<GameObject> spikeList;
    public Transform center;
    public Vector3 prefabSize;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    private void Awake()
    {
        for (int i = 0; i < spikeCount; i++)
        {
            float angle = i * (360.0f / spikeCount);
            float x = radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float y = radius * Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 position = center.position + new Vector3(x, y, 0);
            GameObject newObject = Instantiate(spikePrefab, position, Quaternion.identity);

            // Calculate the rotation angle
            float rotationAngle = angle; 

            // Apply the rotation
            newObject.transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
            newObject.transform.parent = center;
            newObject.transform.localScale = prefabSize;

            spikeList.Add( newObject );
        }
    }
}
