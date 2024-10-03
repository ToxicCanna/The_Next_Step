using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MovementRampage : MonoBehaviour, IPlayer
{
    private RampagePlayerInput rampagePlayerInput;
    private Rigidbody2D rb;
    [SerializeField] private int moveSpeed = 5;
    [SerializeField] private int playerLives = 3;
    [SerializeField] private Image[] livesUI;
    [SerializeField] private GameObject meleeHitboxPrefab;
    private ScoreManager scoreManager;
    [SerializeField] Animator anim;


    // Start is called before the first frame update
    void Awake()
    {
        rampagePlayerInput = new RampagePlayerInput();
        rb = GetComponent<Rigidbody2D>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    private void OnGroundMove(InputValue inputValue)
    {
        rb.velocity = inputValue.Get<Vector2>() * moveSpeed;
    }

    private void OnShoot()
    {
        GameObject hitbox = Instantiate(meleeHitboxPrefab, transform.position, Quaternion.identity);

        anim.SetTrigger("IsPunching");

        Destroy(hitbox, 0.1f);
    }
    public void GetDamage()
    {
        LoseLife();
    }
    public void Collect()
    {
        Debug.Log("collected");
        scoreManager.UpdateScore(500);
    }
    private void LoseLife()
    {
        for (int i = 0; i < livesUI.Length; i++)
        {
            if (i < playerLives - 1)
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
        if (playerLives <= 0)
        {
            Destroy(gameObject);
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
