using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour, IShootable
{
    [SerializeField] private int health;
    [SerializeField] private GameObject explosionPrefab;
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
}
