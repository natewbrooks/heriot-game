using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    LayerMask pickupLayer;
    public bool inPickupRange;
    [SerializeField] float pickupRadius = 2f;
    Collider2D target;
    // Start is called before the first frame update

    private void FixedUpdate() {
        pickupLayer = LayerMask.GetMask("Enemy");
        inPickupRange = Physics2D.OverlapCircle(transform.position, pickupRadius, pickupLayer);
        target = Physics2D.OverlapCircle(transform.position, pickupRadius, pickupLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if(inPickupRange && target.name == "Player") {
            transform.position = Vector2.MoveTowards(transform.position, target.gameObject.transform.position, 10f * Time.deltaTime);
            
            if(transform.position == target.gameObject.transform.position) {
                Destroy(gameObject);
            }
        }
        
    }
}
