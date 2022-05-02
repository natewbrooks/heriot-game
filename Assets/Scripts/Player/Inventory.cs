using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int inventorySize = 24;
    public ItemData[] inventory;
    public int[] inventoryCount;

    
    private void Start() {
        inventory = new ItemData[inventorySize];
        inventoryCount = new int[inventorySize];
    }

    public void AddItem(ItemData item) {
        int index = Array.IndexOf(inventory, item);

        if(index >= 0) {
            inventoryCount[index]+=1;
            return;
        }

        // find first empty spot
        for (int i = 0; i < inventory.Length; i++) {
            if(inventory[i] == null) {
                inventory[i] = item;
                inventoryCount[i] = 1;
                return;
            }
        }
    }

    public void AddItem(ItemData item, int number) {
        int index = Array.IndexOf(inventory, item);

        if(index >= 0) {
            inventoryCount[index]+=number;
            return;
        }
        
        // find first empty spot
        for (int i = 0; i < inventory.Length; i++) {
            if(inventory[i] == null) {
                inventory[i] = item;
                inventoryCount[i] = number;
                return;
            }
        }
    }

    public void ClearInventory() {
        inventory = new ItemData[inventorySize];
        inventoryCount = new int[inventorySize];
    }

    public void RemoveItem(ItemData item) {
        int index = Array.IndexOf(inventory, item);
        inventory[index] = null;
        inventoryCount[index] = 0;
    }

    public void RemoveItem(int index) {
        inventory[index] = null;
        inventoryCount[index] = 0;
    }

    public void SwapItems(int first, int second) {
        ItemData itemTemp = inventory[first];
        int countTemp = inventoryCount[first];

        inventory[first] = inventory[second];
        inventoryCount[first] = inventoryCount[second];
        inventory[second] = itemTemp;
        inventoryCount[second] = countTemp;
    }

    public ItemData GetItem(int index) {
        return inventory[index];
    }

    
}
