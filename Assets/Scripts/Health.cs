using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Health : MonoBehaviour
{
    private GameObject healthbar;
    [SerializeField] private int _vigor = 5; // baby private variable
    [SerializeField] private int _maxVigor = 5;
    [SerializeField] private bool _takeKnockback;
    [SerializeField] private bool _immune;

    private bool deathCall, justHit, stopInvokeCalled;

    // accessors
    public int MaxVigor { get {return _maxVigor; } }  
    public int Vigor { get { return _vigor; } set {_vigor = value; } }
    public bool TakeKnockback { get { return _takeKnockback; } set {_takeKnockback = value; } }
    public bool Immune { get { return _immune; } set { _immune = value; } }

    private void Start() {
        healthbar = transform.GetChild(1).gameObject;
        _vigor = _maxVigor;
    }
    void Update() {
        if(_vigor <= 0) {
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
    public void TakeDamage(float dmg, float knockback = 1, GameObject dealer = null) {
        //Debug.Log("New Health: " + vigor + " - " + (int)dmg + " = " + (vigor-(int)dmg));
        if(!_immune) {
            _immune = true;
            _vigor -= (int) dmg;
            justHit = true;
            stopInvokeCalled = false;
            CancelInvoke("TurnOffHealthbar");
            gameObject.SendMessage("TakeHit");

            if(knockback > 0 && _takeKnockback && dealer != null) {
                StartCoroutine(Knockback(knockback, dealer));
            }
            _immune = false;
        }
        
    }
    IEnumerator Knockback(float knockbackPower, GameObject dealer) {

        Vector3 difference = transform.position - dealer.transform.position;
        difference = difference.normalized * knockbackPower;
        
        if(gameObject != null) {
            transform.DOMove(transform.position + difference, .45f);
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        }

        yield return 0;
    }

    void TurnOffHealthbar() {
        healthbar.SetActive(false);
        stopInvokeCalled = false;
    }
}
