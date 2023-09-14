using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public float damage = 25f;
    public Vector2 knockback = Vector2.zero;
    private void OnTriggerEnter2D(Collider2D other) {
        Damageable damageable = other.GetComponent<Damageable>();
        if(damageable != null)
        {
            damageable.Hit(damage, knockback);
        }
    }
}
