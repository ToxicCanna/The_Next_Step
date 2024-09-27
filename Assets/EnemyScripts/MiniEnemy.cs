using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniEnemy : MonoBehaviour, IEnemy
{
    private Transform[] spawnPoints;
    private Transform player;
    [SerializeField] private EnemyObject EnemyObject;

    [SerializeField] private float randomSpeed;
    public void Initialize(Transform[] spawnPoints, Transform player)
    {
        this.spawnPoints = spawnPoints;
        this.player = player;
        
    }
    private void Start()
    {
       
    }

    private void Update()
    {
        Move();

         
        if (IsOffScreen())
        {
            Respawn();
        }
    }

    public void Move()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, EnemyObject.MoveSpeed * Time.deltaTime);
        }
    }
        public void Respawn()
    {
        int SpawnRandom = Random.Range(0, spawnPoints.Length);
        transform.position = spawnPoints[SpawnRandom].position;
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
        SpawnManager.Instance.EnemyDestroyed();
    }

    private bool IsOffScreen()
    {
        //to check if the player is in the scene or left from the bottom
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        return viewportPosition.y < 0;
    }
}
