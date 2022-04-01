using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Health : MonoBehaviour
{
    private GameObject healthbar;
    [SerializeField] private int vigor = 5; // baby private variable
    [SerializeField] private int maxVigor = 5;
    [SerializeField] private bool kinetic;
    private bool deathCall, justHit, stopInvokeCalled, immune;
    public int MaxVigor {  // called vigor because the class is Health
        get {return maxVigor;} 
    }
    public int Vigor {  // called vigor because the class is Health
        get {return vigor;} 
        set {vigor = value;}
    }
    public bool Kinetic {
        get {return kinetic;} 
    }

    private void Start() {
        healthbar = transform.Find("Healthbar").gameObject;
        vigor = maxVigor;
    }

    // ability to take damage and knockback if this gameobject has a rigidbody, default no knockback
    public void TakeDamage(float dmg, float knockback = 1, GameObject dealer = null) {
        //Debug.Log("New Health: " + vigor + " - " + (int)dmg + " = " + (vigor-(int)dmg));
        if(!immune) {
            immune = true;
            vigor -= (int) dmg;
            justHit = true;
            stopInvokeCalled = false;
            CancelInvoke("TurnOffHealthbar");
            gameObject.SendMessage("TakeHit");

            if(knockback > 0 && kinetic && dealer != null) {
                StartCoroutine(Knockback(knockback, dealer));
            }
            immune = false;
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

        if(healthbar != null && justHit) {
            justHit = false;
            healthbar.SetActive(true); 
            if(!stopInvokeCalled) {
                Invoke("TurnOffHealthbar", 3f);
                stopInvokeCalled = true;
            }
            
        }
    }

    IEnumerator Knockback(float knockbackPower, GameObject dealer) {

        Vector3 difference = transform.position - dealer.transform.position;
        difference = difference.normalized * knockbackPower;
        
        transform.DOMove(transform.position + difference, .75f);
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        yield return 0;
    }


    void TurnOffHealthbar() {
        healthbar.SetActive(false);
        stopInvokeCalled = false;
    }
}
