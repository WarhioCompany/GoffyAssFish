using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Spawning Enemy, telling Enemy to retreat, depending on height 

    public bool activated;

    public GameObject firstEnemy; // preplace enemy (Angel guy: Always there)
    public int curEnemIdx = -1; // current enemy
    public GameObject curEnemy;

    [Header("Enemy Spawn")]
    public Transform EnemySpawn;

    public GameObject[] enemyPrefabs; // the enemys that will spawn
    public float[] enemyChangeHeight; // at what Height an Enemy spawns

    private void Start()
    {
        curEnemy = firstEnemy;
    }

    private void Update()
    {
        if (!activated) { return; }
        //Debug.Log(GameValues.height + " / " + -enemyChangeHeight[curEnemIdx + 1]);
        if (GameValues.height < -enemyChangeHeight[curEnemIdx + 1]) 
        {
            Debug.Log("Spawning new Enemy...");
            if (curEnemIdx < enemyPrefabs.Length-1)
            {
                curEnemIdx++;
            }

            // destroy old enemy
            if (curEnemy != firstEnemy)
            {
                curEnemy.GetComponent<EnemyMovement>().PleaseDie();
            }

            // spawn new enemy
            curEnemy = Instantiate(enemyPrefabs[curEnemIdx], EnemySpawn.position, Quaternion.identity);
            curEnemy.GetComponent<EnemyMovement>().active = true;
        }
    }
}
