using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
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
    [SerializeField] AudioManager audio;

    public TMP_Text winText;

    private Vector2 moveInput;


    // Start is called before the first frame update
    void Awake()
    {
        rampagePlayerInput = new RampagePlayerInput();
        rb = GetComponent<Rigidbody2D>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        audio = FindObjectOfType<AudioManager>();
        winText = GameObject.Find("WinText").GetComponent<TMP_Text>();
        if (winText != null)
        {
            winText.gameObject.SetActive(false); // Hide win text at the start
        }
    }
    private void Update()
    {
        FlipSprite();
    }

    // Update is called once per frame
    private void OnGroundMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
        rb.velocity = inputValue.Get<Vector2>() * moveSpeed;

        if (moveInput.y < 0) // Assuming "S" corresponds to downward movement
        {
            anim.SetTrigger("IsStomping");
        }
        
    }

    private void OnShoot()
    {
        GameObject hitbox = Instantiate(meleeHitboxPrefab, transform.position, Quaternion.identity);

        anim.SetTrigger("IsPunching");

        audio.PlaySFX(audio.PlayerShoot);
        Destroy(hitbox, 0.1f);
    }
    public void GetDamage()
    {
        LoseLife();
    }
    public void Collect()
    {
        Debug.Log("Artifact collected");
        scoreManager.UpdateScore(250);
    }
    public void TrophyCollect()
    {
        Debug.Log("Trophy collected");
        scoreManager.UpdateScore(500);
        if (winText != null)
        {
            winText.text = "You Win!";
            winText.gameObject.SetActive(true);
        }
        StartCoroutine(WaitAndWin());
    }
    public void Score()
    {
        Debug.Log("Destroyed");
        scoreManager.UpdateScore(250);
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
        playerLives--;
        Debug.Log($"Lives = {playerLives}");
        if (playerLives <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void FlipSprite()
    {
        if (moveInput.x > 0)
        {
            // Facing right
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            // Facing left
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    private void Climb()
    {
        rb.gravityScale = 0;
        rb.constraints = RigidbodyConstraints2D.None; // Unfreeze Y position
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        anim.SetBool("OnBuilding", true);
    }
    private void UnfreezePositionY()
    {
        rb.gravityScale = 1;
        anim.SetBool("OnBuilding", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IEnemy>() != null)
        {
            LoseLife();
        }
        if (collision.gameObject.GetComponent<IClimb>() != null)
        {
            Climb();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IClimb>() != null)
        {
            UnfreezePositionY();
        }
    }
    private IEnumerator WaitAndWin()
    {
        yield return new WaitForSeconds(4f); // Wait for 4 seconds
        WinGame();
    }
    public void WinGame()
    {
        SceneManager.LoadScene("EnemyTest");
    }
}
