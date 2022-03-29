using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int vigor; // baby private variable
    private bool deathCall;
    public int Vigor {  // called vigor because the class is Health
        get {return vigor;} 
        set {vigor = value;}
    }

    // ability to take damage and knockback if this gameobject has a rigidbody, default no knockback
    public void TakeDamage(float dmg, float knockback = 0, GameObject obj = null) {
        //Debug.Log("New Health: " + vigor + " - " + (int)dmg + " = " + (vigor-(int)dmg));
        vigor -= (int) dmg;
        gameObject.SendMessage("TakeHit");

        if(knockback > 0 && obj != null && GetComponent<Rigidbody2D>() != null) {
            StartCoroutine(Knockback(knockback, obj));
        }
    }

    void Update() {
        if(vigor <= 0) {
            // send death call only once
            if(!deathCall) {
                gameObject.SendMessage("OnDeath");
                deathCall = true;
            }
        }
    }

    IEnumerator Knockback(float knockbackPower, GameObject obj) {
        float timer = 0;

        while(3 > timer) {
            timer += Time.deltaTime;
            Vector2 direction = (obj.GetComponent<Transform>().position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }
}
