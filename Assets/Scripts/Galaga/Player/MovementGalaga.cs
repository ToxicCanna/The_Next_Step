using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementGalaga : MonoBehaviour, IPlayer
{
    private GalagaPlayerInput galagaPlayerInput;
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private GameObject playerBullet;
    // Start is called before the first frame update
    void Awake()
    {
        galagaPlayerInput = new GalagaPlayerInput();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void OnMovement(InputValue inputValue)
    {
        rb.velocity = inputValue.Get<Vector2>() * moveSpeed;
    }

    private void OnShoot()
    {
        Instantiate(playerBullet, transform.position, Quaternion.identity);
    }
    public void GetDamage()
    {
        LoseLife();
    }
    private void LoseLife()
    {
        Destroy(gameObject);
    }
}
