using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator animator;
    Health health;
    PlayerController controller;
    [SerializeField] private int damage, knockback;

    public int Damage { 
        get { return damage; } 
    }
    public int Knockback { 
        get { return knockback; } 
    }

    private void Start() {
        health = GetComponent<Health>();
        controller = GetComponent<PlayerController>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
    
    private void Update() {
        
    }

    private void TakeHit() {
        animator.SetTrigger("Got Hit?");
    }
    private void OnDeath() {
        controller.isFrozen = true;
        animator.SetTrigger("Got Killed?");
        // open the menu or do something

    }
}
