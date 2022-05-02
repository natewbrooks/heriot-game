using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public ItemData itemData;
    public Inventory playerInventory;

    LayerMask pickupLayer;
    public bool inPickupRange;
    [SerializeField] float pickupRadius = 2f;
    Collider2D target;
    // Start is called before the first frame update

    private void FixedUpdate() {
        pickupLayer = LayerMask.GetMask("Enemy");
        inPickupRange = Physics2D.OverlapCircle(transform.position, pickupRadius, pickupLayer);
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, pickupRadius, pickupLayer);

        if(inPickupRange) {
            foreach (Collider2D col in targets) {
                if(col.gameObject.tag == "Player") {
                    playerInventory = col.gameObject.GetComponent<Inventory>();
                    target = col;
                }
            }
            transform.position = Vector2.MoveTowards(transform.position, target.gameObject.transform.position, 10f * Time.fixedDeltaTime);
            
            if(transform.position == target.gameObject.transform.position) {
                playerInventory.AddItem(itemData);
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}
