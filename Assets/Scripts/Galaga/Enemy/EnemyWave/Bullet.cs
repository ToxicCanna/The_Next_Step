using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float bulletSpeed;

    public void SetSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed;
    }
    private void Update()
    {
        // Move the bullet down
        transform.position += Vector3.down * bulletSpeed * Time.deltaTime;

        // Destroy the bullet if it goes off-screen
        if (transform.position.y < Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y)
        {
            Destroy(gameObject);
        }
    }
}
