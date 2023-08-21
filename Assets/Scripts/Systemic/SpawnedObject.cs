using UnityEngine;

public class SpawnedObject : MonoBehaviour
{
    public string LimitlineTag { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == LimitlineTag)
            Destroy(gameObject);
    }
}