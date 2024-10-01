using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyObject EnemyObject;
    private float bulletTimer;
    

    public void DestroyEnemy()
    {
        
        Destroy(gameObject);
    }

    public void GetDamage()
    {
        //EnemyObject.EnemyCount--;
        GameObject explosion = Instantiate(EnemyObject.ExplosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, 1f);
        DestroyEnemy();
    }

    public void Move()
    {
        transform.Translate(Vector2.right * EnemyObject.MinMoveSpeed * Time.deltaTime);
    }

    public void Respawn()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        throw new System.NotImplementedException();
    }


    void Start()
    {
        
        bulletTimer = Random.Range(EnemyObject.MinShootInterval, EnemyObject.MaxShootInterval);
    }
    void Update()
    {
        Move();
        bulletTimer -= Time.deltaTime;
        if (bulletTimer <= 0)
        {
            Instantiate(EnemyObject.BulletPrefab, transform.position, Quaternion.identity);
            bulletTimer = Random.Range(EnemyObject.MinShootInterval, EnemyObject.MaxShootInterval);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Boundary")
        {
            EnemyObject.MinMoveSpeed *= -1;
        }
    }
}
