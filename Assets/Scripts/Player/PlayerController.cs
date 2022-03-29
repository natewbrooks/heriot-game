using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    public Rigidbody2D rb;
    private float movementHorizontal, movementVertical;
    private float walkSpeed = 150f;
    private float runSpeed = 170f;
    private bool isRunning, isRolling, swordAttacking, isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFrozen) {
            movementHorizontal = Input.GetAxisRaw("Horizontal");
            movementVertical = Input.GetAxisRaw("Vertical"); 

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
        if(!isFrozen) {
            if(isRunning) {
                rb.velocity = new Vector2(movementHorizontal * runSpeed * Time.deltaTime, movementVertical * runSpeed * Time.deltaTime);
            } else {
                rb.velocity = new Vector2(movementHorizontal * walkSpeed * Time.deltaTime, movementVertical * walkSpeed * Time.deltaTime);
            }
        }
        
    }

    void AnimationHandler() {

        if(movementHorizontal < 0) {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        } else if (movementHorizontal > 0) {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        } else {
            transform.localScale = transform.localScale;
        }

        if(movementHorizontal != 0 || movementVertical != 0) {
            animator.SetFloat("Movement Input", 1);
        } else {
            animator.SetFloat("Movement Input", 0);
        }

        animator.SetBool("Is Running", isRunning);
        animator.SetBool("Is Rolling", isRolling);
        animator.SetBool("Is Sword Attacking", swordAttacking);

    }

}
