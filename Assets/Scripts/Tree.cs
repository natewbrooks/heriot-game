using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Tree : Interactable
{
    private Health health;
    [SerializeField] private GameObject drop;
    [SerializeField] private int dropAmount;

    private bool stumpOut = false;

    private void Start() {
        health = GetComponent<Health>();
    }

    public override void OnDeath() {
        if(!stumpOut){
            for (int i = 0; i < dropAmount; i++) {
                GameObject nextDrop = Instantiate(drop, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
                nextDrop.transform.DOJump(nextDrop.transform.position + new Vector3(Random.Range(-.75f,.75f), Random.Range(-1f,1f), 0), .65f, 1, .5f, false);
            }
                
            //set tree sprite inactive and stump active
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
            GetComponent<BoxCollider2D>().enabled = false;
            health.ResetHealth();
            stumpOut = true;
        } else {
            for (int i = 0; i < dropAmount; i++) {
                GameObject nextDrop = Instantiate(drop, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
                nextDrop.transform.DOJump(nextDrop.transform.position + new Vector3(Random.Range(-.75f,.75f), Random.Range(-1f,1f), 0), .65f, 1, .5f, false);
                // somehow throw them up and have them land
            }
            Destroy(gameObject);
        }
        
    }

    public override void TakeHit() {
        // play hit animation with leaves falling
    }

    public override void Interact() {
        health.TakeDamage(3);
    }
}
