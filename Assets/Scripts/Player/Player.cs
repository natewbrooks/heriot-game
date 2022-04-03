using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Animator animator;
    private Health health;
    private PlayerController controller;

    [SerializeField] private int _damage, _knockback;
    [SerializeField] private bool _frozen;

    // accessors
    public int Damage { get {return _damage; } }
    public int Knockback { get { return _knockback; } }
    public bool Frozen { get { return _frozen; } set { _frozen = value; } }

    private void Start() {
        health = GetComponent<Health>();
        controller = GetComponent<PlayerController>();
        animator = transform.GetComponentInChildren<Animator>();
    }

    private void TakeHit() {
        animator.SetTrigger("Got Hit?");
    }
    private void OnDeath() {
        _frozen = true;
        animator.SetTrigger("Got Killed?");
        // open the menu or do something
    }
}
