using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Spawning Enemy, telling Enemy to retreat, depending on height 

    public GameObject firstEnemy; // preplace enemy (Angel guy: Always there)
    private int curEnemIdx = -1; // current enemy
    private GameObject curEnemy;

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
        //Debug.Log(GameValues.height + " / " + -enemyChangeHeight[curEnemIdx + 1]);
        if (GameValues.height < -enemyChangeHeight[curEnemIdx + 1]) 
        {
            Debug.Log("Spawning new Enemy...");
            curEnemIdx++;

            // destroy old enemy
            if (curEnemy != firstEnemy)
            {
                curEnemy.GetComponent<EnemyMovement>().PleaseDie();
            }

            // spawn new enemy
            curEnemy = Instantiate(enemyPrefabs[curEnemIdx], EnemySpawn.position, Quaternion.identity);
        }
    }
}
