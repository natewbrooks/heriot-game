using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector2 movement;
    [SerializeField] private int damage, knockback;

    public int Damage { 
        get { return damage; } 
    }
    public int Knockback { 
        get { return knockback; } 
    }
    private void Start() {
        
    }

    private void Update() {
        AnimationHandler();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Health enemyHealth = other.gameObject.GetComponent<Health>();
        if(enemyHealth != null) {
            // temp damage strategy for enemy
            enemyHealth.TakeDamage(damage, knockback, this.gameObject);
        }
        
    }

    private void TakeHit() {
        // send trigger to animator for hit
    }

    private void OnDeath() {
        // send trigger to animator for death
        // animator handles destroying gameobj
        Destroy(gameObject);
    }

    private void AnimationHandler(){
        if(movement.x < 0) {
            transform.GetChild(0).localScale = new Vector3(-1, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
        } else if (movement.x > 0) {
            transform.GetChild(0).localScale = new Vector3(1, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
        } else {
            transform.GetChild(0).localScale = transform.GetChild(0).localScale;
        }
    }
}
