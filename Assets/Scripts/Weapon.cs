using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponData data;
    private Animator anim;
    private Animator a;

    private void Start() {
        anim = transform.root.GetChild(0).GetComponent<Animator>();
        a = GetComponent<Animator>();
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0) && transform.parent.gameObject.name == "Right Hand") {
            // Debug.Log(transform.parent.gameObject.name + " " + data.type.ToString() + " Attack");
            anim.SetTrigger(transform.parent.gameObject.name + " " + data.type.ToString() + " Attack");
            a.SetTrigger("Attack");
            Debug.Log("Right");

            //when weapon is swapped, override the animations
        }

        if(Input.GetMouseButtonDown(1) && transform.parent.gameObject.name == "Left Hand") {
            // Debug.Log(transform.parent.gameObject.name + " " + data.type.ToString() + " Attack");
            anim.SetTrigger(transform.parent.gameObject.name + " " + data.type.ToString() + " Attack");
            a.SetTrigger("Attack");
            Debug.Log("Left");

            //when weapon is swapped, override the animations
        }
    }
}
