using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    public Transform mainCam;

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

    private void OnDrawGizmos()
    {
        Vector3 posa = transform.position;
        posa.x += XRange;
        posa.y = mainCam.position.y - ShiftHeight;
        posa.z = 0;

        Vector3 posb = transform.position;
        posb.x -= XRange;
        posb.y = mainCam.position.y - ShiftHeight;
        posb.z = 0;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(posa, posb);
    }

    private void Awake()
    {
        Instance = this;
        XRange = GameValues.worldXOffset;
    }

    private Vector3 GetBubblePos(float xRange) {
        float _height = mainCam.position.y - ShiftHeight;
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
    }

    public void Update()
    {
        TrySpawn();
    }
}