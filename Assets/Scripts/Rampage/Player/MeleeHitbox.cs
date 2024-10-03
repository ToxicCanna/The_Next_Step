using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDestroy>(out var destroyable))
        {
            Debug.Log("Hit: " + other.gameObject.name);
            destroyable.GetDamage();

            Destroy(gameObject);
        }
    }
}
