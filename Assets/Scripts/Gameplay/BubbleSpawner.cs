using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    [SerializeField] private GameObject Prefab; // Bubble prefab

    // Delay in seconds
    [SerializeField] private float Delay;
    [SerializeField] private float XRange;
    [SerializeField] private float ScaleRange;
    [SerializeField] private float ShiftHeight;

    [Header("Bubble")]
    // Speed
    [SerializeField] private float BubbleSpeed;

    // Spawned bubbles
    public List<Bubble> CurrentBubbles = new List<Bubble>();

    // Trackers/Counters
    private float cooldownTimer = 0.0f;

    // Constants
    private const int BubbleKeyLength = 10;

    // Static Constructor
    public static BubbleSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    private Vector3 GetBubblePos(float xRange) {
        float _height = Camera.main.transform.position.y - ShiftHeight;
        float randomXPos = Random.Range(-xRange, xRange);
        return new Vector3(randomXPos, _height, 0);
    }

    private Vector3 GetRandomScale(float range) { 
        float randomScale = Random.Range(1, range);
        return new Vector3(randomScale, randomScale);
    }

    private Bubble SetupNewBubble() {
        GameObject bubbleHolder = Instantiate(Prefab);

        bubbleHolder.transform.position = GetBubblePos(XRange);
        bubbleHolder.transform.localScale = GetRandomScale(ScaleRange);

        Bubble bubble = bubbleHolder.AddComponent<Bubble>();
        bubble.Speed = BubbleSpeed;
        bubble.Key = KeyGenerator.Generate(BubbleKeyLength);

        return bubble;
    }

    private void Spawn() { 
        Bubble newBubble = SetupNewBubble();
        CurrentBubbles.Add(newBubble);
    }

    private void TrySpawn() { 
        if(cooldownTimer < Time.time) {
            Spawn();
            cooldownTimer = Time.time + Delay;
        }
    }

    public void Despawn(string key) {
        Bubble bubbleToDespawn = CurrentBubbles.Where(y => y.Key == key).First();
        CurrentBubbles.Remove(bubbleToDespawn);
        Destroy(bubbleToDespawn.gameObject);    
    }

    public void Update()
    {
        TrySpawn();
    }
}