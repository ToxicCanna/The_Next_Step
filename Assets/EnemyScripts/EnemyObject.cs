using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy", fileName = "Enemy")]
public class EnemyObject : ScriptableObject
{
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private float Health;
    [SerializeField] private GameObject enemyType;
}
