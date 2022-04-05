using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Knight : Enemy
{
    
    [SerializeField] private TextMeshProUGUI stateText; 
    

    private void Update() {
        stateText.text = state.ToString();

        RangeDetection();

        switch(state) {
            case State.Approach:
                agent.speed = approachSpeed;
                animator.SetBool("Is Running", false);
                break;
            case State.Attack:
                animator.SetBool("Is Running", false);
                AttackStance();
                movement.dir = Vector3.zero;
                // code
                break;
            case State.Block:
                animator.SetBool("Is Running", false);
                //code
                break;
            case State.Chase:
                agent.speed = runSpeed;
                animator.SetBool("Is Running", true);
                //code
                break;
            case State.Idle:
                animator.SetBool("Is Running", false);
                //code
                break;
            case State.Return:
                agent.SetDestination(movement.startPosition);
                animator.SetBool("Is Running", false);
                break;
            case State.Patrol:
                StartReturnSequence();
                agent.speed = walkSpeed;
                animator.SetBool("Is Running", false);
                //code
                break;
            case State.Walk:
                agent.speed = walkSpeed;
                animator.SetBool("Is Running", false);
                //code
                break;
        }

        
        if(state != State.Approach || state != State.Attack || state != State.Block) {
            // no shield
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        }

        AnimationHandler();
    }

    private void RangeDetection() {
        if(movement.target != null) {
            if(movement.distanceFromTarget < movement.visionRadius*.75f && movement.distanceFromTarget > 3f) {
                state = State.Walk;
            } else if(movement.distanceFromTarget > movement.visionRadius*.75f) {
                state = State.Chase;
            } else if (movement.distanceFromTarget < 3f && movement.distanceFromTarget > 1.25f) {
                state = State.Approach;
            } else if (movement.distanceFromTarget < 1.25f) {
                state = State.Attack;
            }

            if(movement.distanceFromTarget > movement.visionRadius) {
                movement.target = null;
            }
        } else if(movement.target == null && state != State.Return && state != State.Idle) {
            state = State.Patrol;
        }
    }

    private void AnimationHandler() {
        // flip character
        if(agent.velocity.x != 0 || agent.velocity.y != 0) {
            animator.SetFloat("Movement Input", 1);
        } else {
            animator.SetFloat("Movement Input", 0);
        }

        if(movement.dir.x < 0) {
            transform.GetChild(0).localScale = new Vector3(-1, transform.GetChild(0).localScale.y, 0);
        } else if (movement.dir.x > 0) {
            transform.GetChild(0).localScale = new Vector3(1, transform.GetChild(0).localScale.y, 0);
        } else {
            transform.GetChild(0).localScale = transform.GetChild(0).localScale;
        }
    }


    private void AttackStance() {
        if((int) Random.Range(0f, 2f) == 1f && attackCooldown == 2f){
            isBlocking = false;
            agent.speed = approachSpeed;
            animator.SetTrigger("Sword Attack");
            transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        } else {
            isBlocking = true;
            agent.speed = blockSpeed;
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }

        if(isBlocking == true) {
            isBlocking = true;
            agent.speed = blockSpeed;
        }

        if(attackCooldown > 0f) {
               attackCooldown -= Time.deltaTime;
           } else {
               animator.SetTrigger("Sword Attack");
               attackCooldown = 2f;
           }
        }

    public void StartReturnSequence() {
        if(finalSearch) {
            StartCoroutine(movement.ReturnToPost());
        }

        if(agent.hasPath == false) {
            agent.SetDestination(new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(-5f, 5f), 0));
        }
        finalSearch = true;
    }
    
    
    public override void TakeHit() {
        animator.SetTrigger("Got Hit?");
    }

    public override void OnDeath() {
        movement.dir = Vector3.zero;
        health.TakeKnockback = false;
        animator.SetTrigger("Got Killed?");
        // open the menu or do something
    }
}