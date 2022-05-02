using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslucentObject : MonoBehaviour
{
    private bool translucent = false;

    private void Update() {
        if(translucent) {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,.5f);
        } else {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player") {
            translucent = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.name == "Player") {
            translucent = false;
        }
    }
}
