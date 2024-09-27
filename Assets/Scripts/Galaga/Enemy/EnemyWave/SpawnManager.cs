using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject miniEnemyPrefab;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform player;

    private int miniEnemyCount = 0;
    private int maxMiniEnemyCount = 15;
    private int currentEnemyOnScreenCount = 0;
    private int maxEnemyCountOnScreen = 4;

    public static SpawnManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < maxEnemyCountOnScreen; i++)
        {
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        if (currentEnemyOnScreenCount < maxEnemyCountOnScreen)
        {
            GameObject newEnemy = null;

            if (miniEnemyCount < maxMiniEnemyCount)
            {

                newEnemy = Instantiate(miniEnemyPrefab, RandomSpawn().position, Quaternion.identity);
                miniEnemyCount++;

                if (newEnemy != null)
                {

                    var enemyAI = newEnemy.GetComponent<MiniEnemy>();
                    if (enemyAI != null)
                    {
                        enemyAI.Initialize(spawnPoints, player);
                    }

                    currentEnemyOnScreenCount++;
                }
            }

            
        }

    
    }
    public void EnemyDestroyed()
    {
        currentEnemyOnScreenCount--;

        SpawnEnemy();
    }

    private Transform RandomSpawn()
    {
        int randomEnemy = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomEnemy];
    }
}
