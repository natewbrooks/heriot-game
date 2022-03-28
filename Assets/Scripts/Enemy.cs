using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        other.gameObject.GetComponent<Health>().TakeDamage(15, 18, this.gameObject);
    }

    private void TakeHit() {

    }

    private void OnDeath() {
        Destroy(gameObject);
    }
}
