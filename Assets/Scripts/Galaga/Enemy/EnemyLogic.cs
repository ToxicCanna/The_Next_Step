using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour, IShootable
{
    [SerializeField] private int health;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject enemyBullet;
    [SerializeField] private float bulletTimer;
    [SerializeField] private float timeMax = 10;
    [SerializeField] private float timeMin = 3;

    public void GetDamage()
    {
        Explode();
    }
    private void Explode()
    {
        health--;
        if(health <= 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void Start()
    {
        bulletTimer = Random.Range(timeMin, timeMax);
    }
    void Update()
    {
        bulletTimer -= Time.deltaTime;
        if (bulletTimer <= 0)
        {
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            bulletTimer = Random.Range(timeMin, timeMax);
        }
    }
}
