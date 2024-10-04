using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrophyCollect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IPlayer>() != null)
        {
            collision.gameObject.GetComponent<MovementRampage>().TrophyCollect();
            Destroy(gameObject);
        }
    }
}


