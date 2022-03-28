using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    Health colliderHealth;
    Player player;
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D other) {
        colliderHealth = other.gameObject.GetComponent<Health>();
        player = transform.parent.gameObject.GetComponent<Player>();

        if(colliderHealth != null) {
            colliderHealth.TakeDamage(Random.Range(player.Damage-2f, player.Damage+2f), player.Knockback, player.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        colliderHealth = null;    
    }
}
