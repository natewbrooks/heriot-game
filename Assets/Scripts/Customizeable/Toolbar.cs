using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Toolbar : MonoBehaviour
{

    
    [Header("Misc")]
    public static bool toolbarMode;
    
    private static Inventory inventory;
    
    public static ItemData[] toolbar;
    [HideInInspector] public static int[] toolbarCount;

    public static ItemData toolbarItemActive;
    public static int toolbarItemIndex;

    [HideInInspector] public static int toolbarSize = 6;

    public static List<int> validIndexes = new List<int>();

    


    // Start is called before the first frame update
    void Start()
    {
        inventory = GetComponent<Inventory>();
        RefreshToolbar();
    }

    // Update is called once per frame
    void Update()
    {
        // ScrollLogic();
        ToolbarScroll();
    }

    public static void RefreshToolbar() {
        int startOfToolbar = inventory.inventorySize-toolbarSize;
        toolbar = inventory.inventory[startOfToolbar..inventory.inventorySize];
        toolbarCount = inventory.inventoryCount[startOfToolbar..inventory.inventorySize];
        FindFilledSlots();
    }

    public static void ToolbarScroll() {
        RefreshToolbar();
        if(Input.GetKeyDown(KeyCode.RightBracket) && validIndexes.Count > 0) {
            toolbarItemIndex += 1;
            if(toolbarItemIndex >= validIndexes.Count) {
                toolbarItemIndex = 0;
            }
            toolbarItemActive = toolbar[validIndexes[toolbarItemIndex]];
        }

        if(Input.GetKeyDown(KeyCode.LeftBracket) && validIndexes.Count > 0) {
            toolbarItemIndex -= 1;
            if(toolbarItemIndex < 0) {
                toolbarItemIndex = validIndexes.Count-1;
            }
            toolbarItemActive = toolbar[validIndexes[toolbarItemIndex]];
        }
    }


    public static void FindFilledSlots() {
        validIndexes = new List<int>();

        for (int i = 0; i < toolbar.Length; i++) {
            if(toolbar[i] != null) {
                validIndexes.Add(i);
            }
        }
    }
}
