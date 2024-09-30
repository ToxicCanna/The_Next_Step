//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IEnemy
{
    [SerializeField] EnemyObject enemyObject;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Transform[] bulletSpawnPoints;
    private float radius;
    private float angle; 
    private float bulletTimer;
    private bool isRotating;
 

    private void Start()
    {
        radius = Vector3.Distance(transform.position, centerPoint.position);
        bulletTimer = Random.Range(enemyObject.MinShootInterval, enemyObject.MaxShootInterval);
        
        StartCoroutine(BossBehavior());
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
        throw new System.NotImplementedException();
    }

    public void GetDamage()
    {
        
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

        foreach(var spawnPoint in bulletSpawnPoints)
    {
            GameObject bullet = Instantiate(enemyObject.BulletPrefab, spawnPoint.position, Quaternion.identity); 
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>(); 

            // Apply force in all directions
            Vector2 forceDirection = Vector2.zero;

            if (spawnPoint.position.x > transform.position.x) 
            {
                forceDirection = Vector2.right; 
            }
            else if (spawnPoint.position.x < transform.position.x) 
            {
                forceDirection = Vector2.left; 
            }

            if (spawnPoint.position.y > transform.position.y) 
            {
                forceDirection += Vector2.up; 
            }
            else if (spawnPoint.position.y < transform.position.y) 
            {
                forceDirection += Vector2.down; 
            }

            bulletRb.AddForce(forceDirection.normalized * enemyObject.MaxMoveSpeed, ForceMode2D.Impulse); 
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
