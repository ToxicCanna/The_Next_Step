using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MovementGalaga : MonoBehaviour, IPlayer
{
    private GalagaPlayerInput galagaPlayerInput;
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private GameObject playerBullet;
    [SerializeField] private int playerLives = 3;
    [SerializeField] private Image[] livesUI;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Awake()
    {
        galagaPlayerInput = new GalagaPlayerInput();
        rb = GetComponent<Rigidbody2D>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    private void OnMovement(InputValue inputValue)
    {
        rb.velocity = inputValue.Get<Vector2>() * moveSpeed;
    }

    private void OnShoot()
    {
        Instantiate(playerBullet, transform.position, Quaternion.identity);
        audioManager.PlaySFX(audioManager.PlayerShoot);
    }
    public void GetDamage()
    {
        LoseLife();
    }
    private void LoseLife()
    {
        for(int i = 0; i < livesUI.Length; i++)
        {
            if(i < playerLives - 1)
            {
                livesUI[i].enabled = true;
            }
            else
            {
                livesUI[i].enabled = false;
            }
        }
        playerLives -= 1;
        Debug.Log($"Lives = {playerLives}");
        if(playerLives <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("EnemyTest");
            audioManager.PlaySFX(audioManager.PlayerDie);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IEnemy>() != null)
        {
            LoseLife();
        }
    }
}
