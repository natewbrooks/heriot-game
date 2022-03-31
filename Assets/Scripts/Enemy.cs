using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rb;

    void Start() {
        rb = transform.GetChild(0).GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D other) {
        Health enemyHealth = other.gameObject.GetComponent<Health>();
        // temp damage strategy for enemy
        enemyHealth.TakeDamage(15, 18, this.gameObject);
    }

    private void TakeHit() {
        // send trigger to animator for hit
    }

    private void OnDeath() {
        // send trigger to animator for death
        // animator handles destroying gameobj
        Destroy(gameObject);
    }
}
