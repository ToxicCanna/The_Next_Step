using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLogic : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyObject EnemyObject;
    private float bulletTimer;
    private static int enemyCount = 0;

    void Start()
    {
        bulletTimer = Random.Range(EnemyObject.MinShootInterval, EnemyObject.MaxShootInterval);
    }

    void Update()
    {
        Move();
        Shoot();
    }
    public void GetDamage()
    {
        //EnemyObject.EnemyCount--;
        GameObject explosion = Instantiate(EnemyObject.ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        DestroyEnemy();
        
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);

        //load next scene when all the enemies die
        enemyCount++;
        if (enemyCount >= 18)
        {
            SceneManager.LoadScene("BossScene");
        }
    }
    public void Move()
    {
        //constant movement
        transform.Translate(Vector2.right * EnemyObject.MinMoveSpeed * Time.deltaTime);
    }

    public void Respawn()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            //change the direction when boundary touched
            EnemyObject.MinMoveSpeed *= -1;
        }
    }
}
