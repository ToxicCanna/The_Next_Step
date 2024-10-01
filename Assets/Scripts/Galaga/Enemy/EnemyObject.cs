using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "Enemy")]
public class EnemyObject : ScriptableObject
{
    [Header("Enemy Speed")]
    public float MaxMoveSpeed;
    public float MinMoveSpeed;

    [Header("Enemy Type")]
    public GameObject EnemyType;

    [Header("Bullet")]
    public float MinShootInterval;
    public float MaxShootInterval;
    public float ShootingRange;
    public GameObject BulletPrefab;

    [Header("Explosion")]
    public GameObject ExplosionPrefab;

   
}
