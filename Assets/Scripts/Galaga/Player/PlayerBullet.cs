using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
        IEnemy enemy = other.gameObject.GetComponent<IEnemy>();
        if (enemy != null)
        {
            
            enemy.GetDamage();
            Debug.Log("ENEMY IS KILLED");
            Destroy(gameObject);
            scoreManager.UpdateScore(50);
        }
    }
}
