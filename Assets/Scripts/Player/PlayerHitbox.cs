using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    Health colliderHealth;
    Player player;
    [SerializeField] private float damage; // damage the player deals for sword atleast

    private void OnTriggerEnter2D(Collider2D other) {
        colliderHealth = other.gameObject.GetComponent<Health>();
        player = transform.parent.gameObject.GetComponent<Player>();

        if(colliderHealth != null) {    // if the collider has health, deal damage to it
            colliderHealth.TakeDamage(Random.Range(player.Damage-2f, player.Damage+2f), player.Knockback, player.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // forgetty the spagetti as they say (forget the last hit thing)
        colliderHealth = null;    
    }
}
