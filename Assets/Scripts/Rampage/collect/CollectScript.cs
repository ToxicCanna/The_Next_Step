using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IPlayer>() != null)
        {
            collision.gameObject.GetComponent<MovementRampage>().Collect();
            Destroy(gameObject);
        }
    }
}
