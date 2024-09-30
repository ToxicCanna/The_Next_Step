using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniEnemy : MonoBehaviour, IEnemy
{
    private Transform[] spawnPoints;
    private Transform player;
    [SerializeField] private EnemyObject EnemyObject;
    [SerializeField] private Transform bulletSpawn;

    private float randomSpeed;
    private Vector3 targetPosition;
    private Coroutine shootCoroutine;
    public void Initialize(Transform[] spawnPoints, Transform player)
    {
        this.spawnPoints = spawnPoints;
        this.player = player;

        randomSpeed = Random.Range(EnemyObject.MinMoveSpeed, EnemyObject.MaxMoveSpeed);

        shootCoroutine = StartCoroutine(ShootRoutine());
    }
    private void Start()
    {
        targetPosition = new Vector3(player.position.x, transform.position.y - 1f, 0);
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
            //move downards no matter what
            Vector3 downwardMovement = new Vector3(transform.position.x, transform.position.y - (randomSpeed * Time.deltaTime), 0);

            //move the enemy towards the player a lil and keep moving down
            float horizontalAdjustment = Mathf.Lerp(transform.position.x, player.position.x, 0.03f);  
            downwardMovement.x = horizontalAdjustment;

            //current position
            transform.position = downwardMovement;
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

    public void Shoot()
    {
        GameObject bullet = Instantiate(EnemyObject.BulletPrefab, bulletSpawn.position, Quaternion.identity);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.SetSpeed(5f);
        }

        Debug.Log("I AM TRYING TO SHOOT");
    }

    private IEnumerator ShootRoutine()
    {
        while(true)
        {
            if(Vector3.Distance(transform.position, player.position) < EnemyObject.ShootingRange)
            {
                Shoot();
                float waitTime = Random.Range(EnemyObject.MinShootInterval, EnemyObject.MaxShootInterval);
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                // wait before checkig again
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void GetDamage()
    {
        EnemyObject.EnemyCount--;
        GameObject explosion = Instantiate(EnemyObject.ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
    }
    
}
