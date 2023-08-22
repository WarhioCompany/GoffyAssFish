using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFisherman : MonoBehaviour
{
    // using the Hook

    public float offset;
    public Transform spawnHeight;
    public GameObject hookPrefab;

    private GameObject curAttack;

    private void Update()
    {
        if (curAttack == null)
        {
            curAttack = Instantiate(hookPrefab, new Vector3(Random.Range(spawnHeight.position.x - offset, spawnHeight.position.x + offset), spawnHeight.position.y, 0), hookPrefab.transform.rotation);
        }
    }
}
