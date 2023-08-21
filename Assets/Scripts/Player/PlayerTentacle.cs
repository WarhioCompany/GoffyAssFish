using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTentacle : MonoBehaviour
{
    public Transform impact;
    public GameObject obj;

    bool isPulling;

    private void OnDrawGizmos()
    {
        try
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(impact.position, 0.15f);
        } catch { }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object") && !isPulling)
        {
            try
            {
                Destroy(impact.gameObject);
                obj = null;
            } catch { }
            

            Debug.Log("Collided!");
            isPulling = true;

            // save vars and create transform
            obj = collision.gameObject;
            Vector2 pos = Physics2D.Raycast(transform.position, transform.right).point;

            GameObject toSpawn = new GameObject("poi");
            impact = Instantiate(toSpawn, obj.transform).transform;
            impact.transform.position = pos;
        }
    }

    private void Update()
    {
        if (isPulling)
        {
            var dir = impact.position - transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
