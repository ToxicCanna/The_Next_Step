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
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IPlayer>() != null)
        {
            collision.gameObject.GetComponent<IPlayer>().GetDamage();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Boundary")
        {
            Destroy(gameObject);
        }
    }
}
