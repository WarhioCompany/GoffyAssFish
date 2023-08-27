using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Transform cam;

    [Header("General Settings")]
    [SerializeField] private float SpawnMinTime;
    [SerializeField] private float SpawnMaxTime;
    [SerializeField] private float xOffset;
    [SerializeField] private GameObject[] Objects;

    [Header("Light Items")]
    [SerializeField] private Transform l_spawnHeight;
    [SerializeField] private float l_maxWeight;


    [Header("Heavy Items")]
    [SerializeField] private Transform h_spawnHeight;

    // Not-serialized
    private float cooldownTimer = 0.0f;
    private float RotationRange = 180f;

    private float GetRandomDelay(float minDelayTime, float maxDelayTime)
    {
        return Random.Range(minDelayTime, maxDelayTime);
    }

    private GameObject GetRandomPrefab(GameObject[] prefabs)
    {
        int randomIndex = Random.Range(0, prefabs.Length);
        return prefabs[randomIndex];
    }

    private float GetRandomXPos(float xRange)
    {
        return Random.Range(-10, xRange);
    }

    private float GetRandomZRotation(float range)
    {
        return Random.Range(-range, range);
    }

    private void Spawn()
    {
        GameObject prefab = GetRandomPrefab(Objects);
        if (prefab != null)
        {
            GameObject _object = Instantiate(prefab);

            ObjectScript spawned = _object.GetComponent<ObjectScript>();
            _object.transform.position = new Vector3(GetRandomXPos(xOffset), h_spawnHeight.position.y);
            _object.transform.eulerAngles += new Vector3(0, 0, GetRandomZRotation(RotationRange));
        }

        if (prefab != null)
        {
            GameObject _object = Instantiate(prefab);

            ObjectScript spawned = _object.GetComponent<ObjectScript>();
            _object.transform.position = new Vector3(GetRandomXPos(xOffset), h_spawnHeight.position.y);
            _object.transform.eulerAngles += new Vector3(0, 0, GetRandomZRotation(RotationRange));
        }

        prefab = GetRandomPrefab(Objects);
        if (prefab != null)
        {
            GameObject _object = Instantiate(prefab);

            ObjectScript spawned = _object.GetComponent<ObjectScript>();
            _object.transform.position = new Vector3(GetRandomXPos(xOffset), l_spawnHeight.position.y);
            _object.transform.eulerAngles += new Vector3(0, 0, GetRandomZRotation(RotationRange));
        }
    }

    private void TrySpawn()
    {
        if (cooldownTimer < Time.time)
        {
            Spawn();
            cooldownTimer = Time.time + GetRandomDelay(SpawnMinTime, SpawnMaxTime);
        }
    }

    private void Start()
    {
        xOffset = GameValues.worldXOffset;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    private void Update()
    {
        TrySpawn();
    }
}