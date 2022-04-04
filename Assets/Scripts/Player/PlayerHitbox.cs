using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerHitbox : MonoBehaviour
{
    private Health colliderHealth;
    private Player player;
    private Enemy enemy;

    private void Start() {
        player = transform.root.GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
            colliderHealth = other.GetComponent<Health>();
            enemy = other.gameObject.GetComponent<Enemy>();

            if(colliderHealth != null && enemy.team != Enemy.Team.Player && enemy != null) {
                colliderHealth.TakeDamage(player.Damage, player.Knockback, player.gameObject);
            }

    }

    private void OnTriggerExit2D(Collider2D other) {
        // forgetty the spagetti as they say (forget the last hit thing)
        colliderHealth = null;    
    }
}

