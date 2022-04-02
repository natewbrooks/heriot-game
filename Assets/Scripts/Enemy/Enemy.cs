using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    
    [HideInInspector] public Animator animator;
    [HideInInspector] public Health health;
    [HideInInspector] public Movement movement;
    
    public int damage, knockback;
    public bool frozen;
    public Team team;

    public enum Team {
        Player,
        Enemy
    }

    public virtual void Start() {
        animator = transform.GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();
    }


    public abstract void TakeHit();

    public abstract void OnDeath();
}
