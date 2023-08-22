using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private GameObject ExplodeEffect; // Prefab for particle system (?)

    // Not-serialized
    private GameObject currentExplode;
    private readonly Vector3 constantDirection = new Vector3(0, 1);

    // Unique identifier (key)
    public string Key { get; set; }

    // Speed
    public float Speed { get; set; }

    public void Explode()
    {
        currentExplode = Instantiate(ExplodeEffect, transform);
        BubbleSpawner.Instance.Despawn(Key);
    }

    public void Update()
    {
        transform.position += constantDirection * Time.deltaTime * Speed;
    }
}