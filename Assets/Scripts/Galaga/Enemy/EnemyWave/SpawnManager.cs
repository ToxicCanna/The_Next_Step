using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject miniEnemyPrefab;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] public Transform player;

    private int miniEnemyCount = 0;
    private int maxMiniEnemyCount = 15;
    public int currentEnemyOnScreenCount = 0;
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
            //spawn enemy until 15 of those die
            SpawnEnemy();
        }
    }
    private void SpawnEnemy()
    {
        //spawn an enemy if there are less that 4 enemy on screen
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

        if(miniEnemyCount <= maxMiniEnemyCount && currentEnemyOnScreenCount == 0)
        {
            //load new scene when 15 enemies are killed
            SceneManager.LoadScene("EnemyWave2");
        }
        else
        {
            SpawnEnemy();
        }
        
    }

    private Transform RandomSpawn()
    {
        //spawn enemy from any random spawn point
        int randomEnemy = Random.Range(0, spawnPoints.Length);
        return spawnPoints[randomEnemy];
    }
}
