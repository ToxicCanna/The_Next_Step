using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour, IDestroy
{
    [SerializeField] private int health = 3;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        //animations, effects, etc.

        Destroy(gameObject);
    }

}
