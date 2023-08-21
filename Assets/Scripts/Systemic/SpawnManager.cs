using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private float SpawnMinTime;
    [SerializeField] private float SpawnMaxTime;
    [SerializeField] private float XRange;
    [SerializeField] private string LimitlineTag;
    [SerializeField] private GameObject[] Objects;

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
        return Random.Range(-xRange, xRange);
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
            SpawnedObject spawned = _object.AddComponent<SpawnedObject>();
            spawned.LimitlineTag = LimitlineTag;
            _object.transform.position = new Vector3(GetRandomXPos(XRange), Camera.main.transform.position.y);
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

    private void Update()
    {
        TrySpawn();
    }
}