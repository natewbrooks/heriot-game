using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    
    [HideInInspector] public Animator animator;
    [HideInInspector] public Health health;
    [HideInInspector] public Movement movement;
    [HideInInspector] public UnityEngine.AI.NavMeshAgent agent;
    
    public Team team;
    public State state = State.Idle;


    [Header("Arsenal")]
    public WeaponData leftHand;
    public WeaponData rightHand;

    [HideInInspector] public bool isBlocking;
    [HideInInspector] public float attackCooldown = 2f;

    [Header("Movement")]
    public float walkSpeed = 2f;
    public float runSpeed = 3f;
    public float approachSpeed = 1.25f;
    public float blockSpeed = 1f;

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
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }


    public abstract void TakeHit();

    public abstract void OnDeath();
}
