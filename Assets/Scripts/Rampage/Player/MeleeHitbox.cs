using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDestroy destroy = other.gameObject.GetComponent<IDestroy>();
        if (destroy != null)
        {
            Debug.Log("Hit: " + other.gameObject.name);
            destroy.GetDamage();
        }
    }
}
