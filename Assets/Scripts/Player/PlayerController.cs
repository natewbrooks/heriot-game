using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Health health;
    private Player player;
    private Equipment equipment;

    private Vector2 movement;
    private float walkSpeed = 3f, blockSpeed = 1f, runSpeed = 4f, speed;
    private bool isRunning, isBlocking, canHit = true;
    private float attackRate = 4f, nextAttackTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        health = GetComponent<Health>();
        player = GetComponent<Player>();
        equipment = GetComponent<Equipment>();

        speed = walkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.Frozen) {    
            if(Input.GetKey(KeyCode.Space)) {
                isRunning = true;
            } else {
                isRunning = false;
            }

            if(Input.GetKeyDown(KeyCode.LeftAlt)) {
                animator.SetTrigger("Roll");
            }

            // attacking
            if(Time.time >= nextAttackTime && canHit) {
                if(Input.GetMouseButtonDown(0)) {
                    animator.SetTrigger("Sword Attack");
                    nextAttackTime = Time.time + 1f / attackRate;
                }
            }

            // blocking
            if(Input.GetMouseButton(1) && (equipment.LeftHand == Equipment.Arsenal.Shield)) {
                isBlocking = true;
                health.TakeKnockback = false;
                health.Immune = true;
                canHit = false;
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true); // temporary shield, need to make animation like sword
                isRunning = false;
            } else {
                isBlocking = false;
                health.TakeKnockback = true;
                health.Immune = false;
                canHit = true;
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
            
            AnimationHandler();
        }
    }

    void FixedUpdate() {
        if(!player.Frozen) {
            Move();         
        }
    }

    void AnimationHandler() {
        
        // flip character and children
        if(movement.x < 0) {
            transform.GetChild(0).localScale = new Vector3(-1, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
        } else if (movement.x > 0) {
            transform.GetChild(0).localScale = new Vector3(1, transform.GetChild(0).localScale.y, transform.GetChild(0).localScale.z);
        } else {
            transform.GetChild(0).localScale = transform.GetChild(0).localScale;
        }

        if(movement.x != 0 || movement.y != 0) {
            animator.SetFloat("Movement Input", 1);
        } else {
            animator.SetFloat("Movement Input", 0);
        }

        animator.SetBool("Is Running", isRunning);

    }
    

    void Move() {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if(isRunning) {
            speed = runSpeed;
        } else if (isBlocking) {
            speed = blockSpeed;
        } else {
            speed = walkSpeed;
        }

        transform.position += (Vector3)movement * speed * Time.fixedDeltaTime;
    }

}