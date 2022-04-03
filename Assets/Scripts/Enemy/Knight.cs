using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Knight : Enemy
{
    // AI BEHAVIOR (STATES)
    private enum State {
        Approach,
        Attack,
        Block,
        Chase,
        Idle,
        Patrol,
        Return,
        Walk
    }
    
    private State state = State.Idle;
    [SerializeField] private TextMeshProUGUI stateText; 

    private float timeUntilReturn = 4f;

    private void Update() {
        stateText.text = state.ToString();

        RangeDetection();

        switch(state) {
            case State.Approach:
                movement.repathEnabled = true;
                movement.speed = approachSpeed;
                animator.SetBool("Is Running", false);
                frozen = false;
                break;
            case State.Attack:
                movement.repathEnabled = false;
                animator.SetBool("Is Running", false);
                movement.dir = Vector3.zero;
                frozen = true;
                // code
                break;
            case State.Block:
                movement.repathEnabled = false;
                animator.SetBool("Is Running", false);
                frozen = true;
                //code
                break;
            case State.Chase:
                movement.repathEnabled = true;
                movement.speed = runSpeed;
                animator.SetBool("Is Running", true);
                frozen = false;
                //code
                break;
            case State.Idle:
                movement.repathEnabled = false;
                animator.SetBool("Is Running", false);
                frozen = true;
                //code
                break;
            case State.Return:
                movement.repathEnabled = false;
                animator.SetBool("Is Running", false);
                frozen = false;
                movement.SetNewPath(movement.startPosition);
                break;
            case State.Patrol:
                movement.repathEnabled = false;
                movement.speed = walkSpeed;
                animator.SetBool("Is Running", false);
                frozen = false;
                //code
                break;
            case State.Walk:
                movement.repathEnabled = true;
                movement.speed = walkSpeed;
                animator.SetBool("Is Running", false);
                frozen = false;
                //code
                break;
        }

        if(target == null && movement.donePath && state == State.Patrol && Vector2.Distance(transform.position, movement.startPosition) > .15f) {
           if(timeUntilReturn > 0f) {
               timeUntilReturn -= Time.deltaTime;
           } else {
               state = State.Return;
               timeUntilReturn = 4f;
           }
        } else if (Vector2.Distance(transform.position, movement.startPosition) < .15f) {
            state = State.Idle;
        }


        AnimationHandler();

        /* check for target in proximity
            path find and walk towards them
                if distance is small enough stop movement and engage attack stance
                sometimes attack, sometimes block; account for reaction time */

        /* if target get out of range, chase after him. If he successfully leaves the range then 
        patrol the area looking for him */
    }

    private void FixedUpdate() {
        targetInRange = Physics2D.OverlapCircle(transform.position, visionRadius, LayerMask.GetMask("Player"));
        if(targetInRange) {
            target = Physics2D.OverlapCircle(transform.position, visionRadius, LayerMask.GetMask("Player")).transform;
            distanceFromTarget = Vector2.Distance(transform.position, target.position);
        } else {
            target = null;
        }

        movement.targetPosition = target;
    }

    private void RangeDetection() {
        if(target != null) {
            if(distanceFromTarget < 7f && distanceFromTarget > 3f) {
                state = State.Walk;
            } else if(distanceFromTarget > 7f && distanceFromTarget <= visionRadius) {
                state = State.Chase;
            } else if (distanceFromTarget < 3f && distanceFromTarget > 1.25f) {
                state = State.Approach;
            } else if (distanceFromTarget < 1.25f) {
                state = State.Attack;
            }
        } else if(target == null && state != State.Return) {
            state = State.Patrol;
        }
    }

    public override void TakeHit() {
        animator.SetTrigger("Got Hit?");
    }

    public override void OnDeath() {
        frozen = true;
        animator.SetTrigger("Got Killed?");
        // open the menu or do something
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, visionRadius);    
    }

    private void AnimationHandler() {
        // flip character


        if(movement.velocity.x != 0 || movement.velocity.y != 0) {
            animator.SetFloat("Movement Input", 1);
        } else {
            animator.SetFloat("Movement Input", 0);

        }

        if(movement.dir.x < 0) {
            transform.GetChild(0).localScale = new Vector3(-1, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
        } else if (movement.dir.x > 0) {
            transform.GetChild(0).localScale = new Vector3(1, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
        } else {
            transform.GetChild(0).localScale = transform.GetChild(0).localScale;
        }
    }

}
