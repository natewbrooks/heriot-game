using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    
    [HideInInspector] public Animator animator;
    [HideInInspector] public Health health;
    [HideInInspector] public Movement movement;
    
    public Team team;
    public State state = State.Idle;

    [Header("Arsenal")]
    public Equipment.Arsenal leftHand;
    public Equipment.Arsenal rightHand;
    public int damage;
    public int knockback;
    [HideInInspector] public bool isBlocking;
    [HideInInspector] public float attackCooldown = 2f;

    [Header("Movement")]
    public bool frozen;    
    public float walkSpeed = 2f;
    public float runSpeed = 3f;
    public float approachSpeed = 1.25f;
    public float blockSpeed = 1f;
    public bool finalSearch = true;

    public enum Team {
        Player,
        Enemy
    }

    public enum State {
        Approach,
        Attack,
        Block,
        Chase,
        Idle,
        Patrol,
        Return,
        Walk
    }

    public virtual void Start() {
        animator = transform.GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
        movement = GetComponent<Movement>();
    }


    public abstract void TakeHit();

    public abstract void OnDeath();
}
