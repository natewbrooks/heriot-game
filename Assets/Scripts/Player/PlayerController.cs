using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Vector2 movement;
    private float walkSpeed = 3f, blockSpeed = 1f, runSpeed = 4f;
    private bool isRunning, isRolling, swordAttacking, isBlocking;
    public bool isFrozen;

    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isFrozen) {    
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

            // attacking
            if(Input.GetMouseButtonDown(0) && !swordAttacking) {
                swordAttacking = true;
                isBlocking = false;
            } else {
                swordAttacking = false;
            }

            // blocking
            if(Input.GetMouseButton(1)) {
                isBlocking = true;
                isRunning = false;
                Debug.Log(isBlocking);
            } else {
                isBlocking = false;
            }
            

            
            AnimationHandler();
        }
    }

    void FixedUpdate() {
        if(!isFrozen) {
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
        animator.SetBool("Is Rolling", isRolling);
        animator.SetBool("Is Sword Attacking", swordAttacking);

    }
    

    void Move() {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
            
        if(isRunning) {
            transform.position = new Vector2(transform.position.x + movement.x * runSpeed * Time.fixedDeltaTime, transform.position.y + movement.y * runSpeed * Time.fixedDeltaTime);
        } else if (isBlocking) {
            transform.position = new Vector2(transform.position.x + movement.x * blockSpeed * Time.fixedDeltaTime, transform.position.y + movement.y * runSpeed * Time.fixedDeltaTime);

        } else {
            transform.position = new Vector2(transform.position.x + movement.x * walkSpeed * Time.fixedDeltaTime, transform.position.y + movement.y * runSpeed * Time.fixedDeltaTime);

        }
    }

}