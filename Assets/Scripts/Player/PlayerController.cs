using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    public Rigidbody2D rb;
    private Vector2 movement;
    private float walkSpeed = 150f;
    private float runSpeed = 170f;
    private bool isRunning, isRolling, swordAttacking;
    public bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFrozen) {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if(Input.GetKey(KeyCode.Space)) {
                isRunning = true;
            } else {
                isRunning = false;
            }

            if(Input.GetKeyDown(KeyCode.LeftAlt)) {
                isRolling = true;
            } else {
                isRolling = false;
            }

            if(Input.GetKeyDown(KeyCode.Z) && !swordAttacking) {
                swordAttacking = true;
            } else {
                swordAttacking = false;
            }
            
            
            AnimationHandler();
        } else {
            rb.velocity = Vector2.zero;
        }
    }

    void FixedUpdate() {
            if(isRunning) {
                rb.velocity = new Vector2(movement.x * runSpeed * Time.deltaTime, movement.y * runSpeed * Time.deltaTime);
            } else {
                rb.velocity = new Vector2(movement.x * walkSpeed * Time.deltaTime, movement.y * walkSpeed * Time.deltaTime);
            }
    }

    void AnimationHandler() {

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
        animator.SetBool("Is Rolling", isRolling);
        animator.SetBool("Is Sword Attacking", swordAttacking);

    }

}