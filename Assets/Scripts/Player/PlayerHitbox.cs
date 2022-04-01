using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;

public class PlayerHitbox : MonoBehaviour
{
    Health colliderHealth;
    Player player;

    private void Start() {
        player = transform.root.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
            colliderHealth = other.GetComponent<Health>();

            if(colliderHealth != null) {
                colliderHealth.TakeDamage(player.Damage, player.Knockback, player.gameObject);
            }

    }

    private void OnTriggerExit2D(Collider2D other) {
        // forgetty the spagetti as they say (forget the last hit thing)
        colliderHealth = null;    
    }
}

