//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyObject enemyObject;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform[] bulletSpawnPoints;


    private float radius;
    private float angle; 
    private float bulletTimer;
    private bool isRotating;


    [SerializeField] private int bossHealth = 30;
    [SerializeField] private Slider healthBar;


    private void Start()
    {
        //radius is different between center point and enemy
        radius = Vector3.Distance(transform.position, centerPoint.position);
        bulletTimer = Random.Range(enemyObject.MinShootInterval, enemyObject.MaxShootInterval);
        
        StartCoroutine(BossBehavior());

        healthBar.maxValue = bossHealth;
        healthBar.value = bossHealth;
    }

    private void Update()
    {
        if(!isRotating)
        {
            Move();
        }
        bulletTimer -= Time.deltaTime;

        if (isRotating)
        {
            Debug.Log("Rotating!!!!!!!!!!!!!"); 
            if (bulletTimer <= 0)
            {
                Shoot();
                bulletTimer = Random.Range(enemyObject.MinShootInterval, enemyObject.MaxShootInterval);
            }
            RotateInPlace(); 
        }
    }
    public void DestroyEnemy()
    {
        Destroy(gameObject);

    }

    public void GetDamage()
    {
        Debug.Log("Health = " + bossHealth);

        //damage set to 1
        BossDamage(1);
        if (bossHealth <= 0)
        {
            //destroy enemy when health is less than 0
            DestroyEnemy(); 
            Debug.Log("Enemy destroyed!");
        }
        
    }

    public void BossDamage(int damage)
    {
        //updating health bar
        bossHealth -= damage;
        healthBar.value = bossHealth;
        
    }
    public void Move()
    {
        //moving in circular motion
        angle += enemyObject.MinMoveSpeed * Time.deltaTime;
        float x = centerPoint.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float y = centerPoint.position.y + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        transform.position = new Vector3(x, y, transform.position.z);

    }

    public void Respawn()
    {
        throw new System.NotImplementedException();
    }

    public void Shoot()
    {
        foreach (var spawnPoint in bulletSpawnPoints)
        {
            GameObject bullet = Instantiate(enemyObject.BulletPrefab, spawnPoint.position, Quaternion.identity);
            Vector2 direction = (spawnPoint.position - transform.position).normalized;
            bullet.transform.up = direction;
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetSpeed(5f); 
            }
        }

    }

    private IEnumerator BossBehavior()
    {
        while (true)
        {
            // Wait before rotating
            yield return new WaitForSeconds(3f);

            
            isRotating = true;
            //Debug.Log("I AM MOVING");

            // Rotate
            yield return new WaitForSeconds(2f); 
            isRotating = false; 
            Debug.Log("SHOOOR");
        }
    }

    private void RotateInPlace()
    {
        //Rotate but in place
        transform.Rotate(0, 0, enemyObject.MaxMoveSpeed * Time.deltaTime);
    }
    
}
