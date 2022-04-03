using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    
    [HideInInspector] public Animator animator;
    [HideInInspector] public Health health;
    [HideInInspector] public Movement movement;
    
    public Team team;
    [Header("Arsenal")]
    public Equipment.Arsenal leftHand;
    public Equipment.Arsenal rightHand;
    public int damage;
    public int knockback;
    [Header("Movement")]
    public bool frozen;    
    public float walkSpeed = 2f;
    public float runSpeed = 3f;
    public float approachSpeed = 1.25f;
    public float blockSpeed = 1f;
    [Header("Intelligence")]
    public float visionRadius = 5f;

    [HideInInspector] public Transform target;
    [HideInInspector] public bool targetInRange;
    [HideInInspector] public float distanceFromTarget;
    


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
