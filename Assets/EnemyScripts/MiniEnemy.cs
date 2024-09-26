using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemy : MonoBehaviour, IEnemy
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform Player;

    private Vector2 EnemyTarget;
    public EnemyObject EnemyObject;

    private void Start()
    {
        EnemyTarget = Player.position;
    }

    private void Update()
    {
        Move();

        //to check if the player is in the scene or left from the bottom 
        if(transform.position.y < Screen.height)
        {
            Respawn();
        }
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, EnemyTarget, EnemyObject.MoveSpeed * Time.deltaTime);
    }

    public void Respawn()
    {
        int SpawnRandom = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[SpawnRandom].position;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
