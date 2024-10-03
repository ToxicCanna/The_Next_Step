using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour, IDestroy
{
    [SerializeField] private int health = 3;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject trophy;
    [SerializeField] private Transform trophyLocation;


    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
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

        if (player != null)
        {
            player.GetComponent<MovementRampage>().Score();
        }

        float randomValue = Random.Range(0f, 1f);

        if (randomValue < 0.25f)
        {
            Instantiate(trophy, trophyLocation.position, Quaternion.identity);
            Debug.Log($"Trophy instantiated! {randomValue} ");
        }
        else
        {
            Debug.Log("No trophy this time.");
        }
    }
    public void AddScore()
    {
        player.GetComponent<MovementRampage>().Score();
    }
}
