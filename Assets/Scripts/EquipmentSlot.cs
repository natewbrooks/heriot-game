using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeField] public Equipment.Arsenal weapon;
    EquipmentMenu menu;

    private void Awake() {
        menu = FindParentWithTag(gameObject, "Equipment Menu").GetComponent<EquipmentMenu>();
        transform.GetChild(0).GetComponent<Image>().sprite = UIEngine.Instance.equipmentIcons[(int)weapon];
    }

    public void ChangeWeapon() {
        menu.UpdateEquipment(this);
    }

    public static GameObject FindParentWithTag(GameObject childObject, string tag) {
        Transform t = childObject.transform;
        while (t.parent != null) {
            if (t.parent.tag == tag) {
                return t.parent.gameObject;
            }
                t = t.parent.transform;
        }

        return null; // Could not find a parent with given tag.
    }
    
}
