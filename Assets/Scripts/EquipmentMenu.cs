using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentMenu : MonoBehaviour
{
    [SerializeField] private Equipment equipment;
    [SerializeField] private WeaponSlot[] weaponSlots = {};

    [SerializeField] private GameObject leftSlots;
    [SerializeField] private GameObject rightSlots;
    [SerializeField] private GameObject leftEquipment;
    [SerializeField] private GameObject rightEquipment;

    private int currentSlotIndex;
    private WeaponSlot currentSlot;
    private bool currentSlotOnLeft;
    private GameObject hiddenSlot;

    void Start() {
        for (int i = 0; i < weaponSlots.Length; i++) {
            weaponSlots[i].currentEquipped = equipment.weaponsEquipped[i];
            weaponSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = UIEngine.Instance.equipmentIcons[(int)weaponSlots[i].currentEquipped];
        }
    }

    public void UpdateEquipment(EquipmentSlot slot) {
        // changes the current weapon slot to be the proposed weapon
        currentSlot.currentEquipped = slot.weapon;
        // changes the corresponding slot in the weaponsEquipped array in players equipment to the proposed slot
        equipment.weaponsEquipped[currentSlotIndex] = slot.weapon;
        // updates the slots sprite
        currentSlot.transform.GetChild(0).GetComponent<Image>().sprite = UIEngine.Instance.equipmentIcons[(int)currentSlot.currentEquipped];
    
        // checks to see if the currentSlot is on left side, and if the players left
        // players left hand if he changed what he has currently equipped, vice versa for right
        if(currentSlotOnLeft && equipment.LeftHandIndex == currentSlotIndex) {
            equipment.LeftHand = slot.weapon;
        } else if (!currentSlotOnLeft && equipment.RightHandIndex == currentSlotIndex) {
            equipment.RightHand = slot.weapon;
        }

        CloseSlots();
    }

    public void CurrentEquipmentSelection(WeaponSlot slot) {

        // if you hit the same weapon slot twice, exits out and resets
        if(currentSlot == slot) {
            leftSlots.SetActive(false);
            rightSlots.SetActive(false);
            currentSlot = null;
        } else {
            currentSlot = slot;
            
            for (int i = 0; i < weaponSlots.Length; i++)
            {
                currentSlotIndex = i;
                // checks to see if the current slot is first or second in the array
                if(currentSlot == weaponSlots[i] && i <= 1) {
                    currentSlotOnLeft = true;
                    // DeleteSlotsAlreadyInUse();
                    leftSlots.SetActive(true);
                    rightSlots.SetActive(false);
                    break;
                } else if (currentSlot == weaponSlots[i] && i >= 2) { // checks to see if the current slot is third or fourth
                    currentSlotOnLeft = false;
                    // DeleteSlotsAlreadyInUse();
                    rightSlots.SetActive(true);
                    leftSlots.SetActive(false);
                    break;
                }
            }
        }
    }


    private void DeleteSlotsAlreadyInUse() {
        // unhide the last hidden slot
        if(hiddenSlot != null) {
            hiddenSlot.SetActive(true);
        }

        // hide the weapon in the adjacent slot, prevents having two swords in one hand etc
        if(currentSlotIndex == 0) {
            hiddenSlot = FindSlotWithWeapon(weaponSlots[1].currentEquipped, "left");
        } else if (currentSlotIndex == 1) {
            hiddenSlot = FindSlotWithWeapon(weaponSlots[0].currentEquipped, "left");
        } else if (currentSlotIndex == 2) {
            hiddenSlot = FindSlotWithWeapon(weaponSlots[3].currentEquipped, "right");
        } else if (currentSlotIndex == 3) {
            hiddenSlot = FindSlotWithWeapon(weaponSlots[2].currentEquipped, "right");
        }

        hiddenSlot.SetActive(false);
    }

    private GameObject FindSlotWithWeapon(Equipment.Arsenal weapon, string side) {
        if(side == "left") {
            for (int i = 0; i < leftSlots.transform.childCount; i++) {
               if(leftSlots.transform.GetChild(i).GetComponent<EquipmentSlot>().weapon == weapon) {
                   return leftSlots.transform.GetChild(i).gameObject;
               }
            }
        } else if (side == "right") {
            for (int i = 0; i < rightSlots.transform.childCount; i++) {
               if(rightSlots.transform.GetChild(i).GetComponent<EquipmentSlot>().weapon == weapon) {
                   return rightSlots.transform.GetChild(i).gameObject;
               }
            }
        }
        return null;
    }

    public void CloseSlots() {
        // closes the equipment slots when the menu isnt active, and deselect weapon slot
        currentSlot = null;
        rightSlots.SetActive(false);
        leftSlots.SetActive(false);
    }
}
