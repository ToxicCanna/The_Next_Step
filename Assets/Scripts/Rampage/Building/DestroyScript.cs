using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour, IDestroy
{
    [SerializeField] private int health = 3;
    [SerializeField] GameObject player;
    public void GetDamage()
    {
        health--; // Decrease health by 1
        Debug.Log($"{gameObject.name} took damage! Remaining health: {health}");

        if (health <= 0)
        {
            Destroy();
        }
    }
    public void Destroy()
    {
        Debug.Log($"{gameObject.name} is destroyed!");

        Destroy(gameObject);

        player.GetComponent<MovementRampage>().Score();
    }
    public void AddScore()
    {
        player.GetComponent<MovementRampage>().Score();
    }
}
