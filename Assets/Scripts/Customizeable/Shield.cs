using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    Health health;

    private void Start() {
        transform.root.GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Weapon") {
            health.Immune = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        health.Immune = true;
    }
}
