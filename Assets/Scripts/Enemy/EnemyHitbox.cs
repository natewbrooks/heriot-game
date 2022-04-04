using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private Health colliderHealth;
    private Enemy thisEnemy;

    private void Start() {
        thisEnemy = transform.root.GetComponent<Enemy>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        
            colliderHealth = other.GetComponent<Health>();

            if(other.tag == "Player" && colliderHealth != null) {
                if(thisEnemy.team != Enemy.Team.Player) {
                    colliderHealth.TakeDamage(thisEnemy.damage, thisEnemy.knockback, this.gameObject);
                }
            } 
            
            if(other.tag == "Enemy") {
                if((other.gameObject.GetComponent<Enemy>().team != thisEnemy.team) && colliderHealth != null) {
                    colliderHealth.TakeDamage(thisEnemy.damage, thisEnemy.knockback, this.gameObject);
                }
            }

    }

    private void OnTriggerExit2D(Collider2D other) {
        // forgetty the spagetti as they say (forget the last hit thing)
        colliderHealth = null;    
    }
}
