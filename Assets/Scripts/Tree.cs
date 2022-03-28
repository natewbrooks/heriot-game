using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Health health;
    [SerializeField] private GameObject stump;
    [SerializeField] private GameObject drop;
    [SerializeField] private int dropAmount;

    private void Start() {
        health = GetComponent<Health>();
    }

    private void OnDeath() {
        Instantiate(stump, new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y, transform.GetChild(0).transform.position.z), transform.GetChild(0).transform.rotation);
        for (int i = 0; i < dropAmount; i++)
        {
           GameObject nextDrop = Instantiate(drop, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);
           // somehow throw them up and have them land
        }
        
        Destroy(gameObject);
    }

    private void TakeHit() {
        
    }
}
