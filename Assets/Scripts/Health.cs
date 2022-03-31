using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Health : MonoBehaviour
{
    private GameObject healthbar;
    [SerializeField] private int vigor = 5; // baby private variable
    [SerializeField] private int maxVigor = 5;
    private bool deathCall, justHit;
    public int MaxVigor {  // called vigor because the class is Health
        get {return maxVigor;} 
    }
    public int Vigor {  // called vigor because the class is Health
        get {return vigor;} 
        set {vigor = value;}
    }

    private void Start() {
        try {
            healthbar = transform.Find("Healthbar").gameObject;
        } catch (Exception e) {
            healthbar = null;
        }
        vigor = maxVigor;
    }

    // ability to take damage and knockback if this gameobject has a rigidbody, default no knockback
    public void TakeDamage(float dmg, float knockback = 0, GameObject obj = null) {
        //Debug.Log("New Health: " + vigor + " - " + (int)dmg + " = " + (vigor-(int)dmg));
        vigor -= (int) dmg;
        justHit = true;
        CancelInvoke("TurnOffJustHit");
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

        if(healthbar != null) {
            if(justHit) {
                healthbar.SetActive(true);
                Invoke("TurnOffHealthbar", 3f);
            } else {
                healthbar.SetActive(false);
            }
        }
    }

    IEnumerator Knockback(float knockbackPower, GameObject obj) {
        float timer = 0;

        while(3 > timer) {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - transform.position).normalized;
            transform.GetComponent<Rigidbody2D>().AddForce(-direction * knockbackPower);
        }

        yield return 0;
    }


    void TurnOffHealthbar() {
        justHit = false;
    }
}
