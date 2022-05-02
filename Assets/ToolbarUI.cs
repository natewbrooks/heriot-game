using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarUI : MonoBehaviour
{

    public int firstSlotItemIndex = 0, secondSlotItemIndex = 1, thirdSlotItemIndex = 2;
    public Image[] slots = new Image[3];
    
    private void Update() {
        RefreshToolbarUI();
    }

    public void RefreshToolbarUI() {
        if(Toolbar.validIndexes.Count > 0 && Input.GetKeyDown(KeyCode.RightBracket)) {
            firstSlotItemIndex+=1;
            secondSlotItemIndex+=1;
            thirdSlotItemIndex+=1;

            if(firstSlotItemIndex >= Toolbar.validIndexes.Count) {
                firstSlotItemIndex = 0;
            }
            if(secondSlotItemIndex >= Toolbar.validIndexes.Count) {
                secondSlotItemIndex = 0;
            }
            if(thirdSlotItemIndex >= Toolbar.validIndexes.Count) {
                thirdSlotItemIndex = 0;
            }

            slots[0].sprite = Toolbar.toolbar[Toolbar.validIndexes[firstSlotItemIndex]].itemSprite;
            slots[1].sprite = Toolbar.toolbar[Toolbar.validIndexes[secondSlotItemIndex]].itemSprite;
            slots[2].sprite = Toolbar.toolbar[Toolbar.validIndexes[thirdSlotItemIndex]].itemSprite;
        }

        if(Toolbar.validIndexes.Count > 0 && Input.GetKeyDown(KeyCode.LeftBracket)) {
            firstSlotItemIndex-=1;
            secondSlotItemIndex-=1;
            thirdSlotItemIndex-=1;

            if(firstSlotItemIndex < 0) {
                firstSlotItemIndex = Toolbar.validIndexes.Count-1;
            }
            if(secondSlotItemIndex < 0) {
                secondSlotItemIndex = Toolbar.validIndexes.Count-1;
            }
            if(thirdSlotItemIndex < 0) {
                thirdSlotItemIndex = Toolbar.validIndexes.Count-1;
            }

            slots[0].sprite = Toolbar.toolbar[Toolbar.validIndexes[firstSlotItemIndex]].itemSprite;
            slots[1].sprite = Toolbar.toolbar[Toolbar.validIndexes[secondSlotItemIndex]].itemSprite;
            slots[2].sprite = Toolbar.toolbar[Toolbar.validIndexes[thirdSlotItemIndex]].itemSprite;
        }
    }

    // void MoveRight() {
    //         firstSlotItemIndex+=1;
    //         secondSlotItemIndex+=1;
    //         thirdSlotItemIndex+=1;

    //         if(firstSlotItemIndex >= validIndexes.Count) {
    //             firstSlotItemIndex = 0;
    //         }
    //         if(secondSlotItemIndex >= validIndexes.Count) {
    //             secondSlotItemIndex = 0;
    //         }
    //         if(thirdSlotItemIndex >= validIndexes.Count) {
    //             thirdSlotItemIndex = 0;
    //         }

    //         slots[0].sprite = inventory.toolbar[validIndexes[firstSlotItemIndex]].itemSprite;
    //         slots[1].sprite = inventory.toolbar[validIndexes[secondSlotItemIndex]].itemSprite;
    //         slots[2].sprite = inventory.toolbar[validIndexes[thirdSlotItemIndex]].itemSprite;
            
    // }

    // void MoveLeft() {
    //         firstSlotItemIndex-=1;
    //         secondSlotItemIndex-=1;
    //         thirdSlotItemIndex-=1;

    //         if(firstSlotItemIndex < 0) {
    //             firstSlotItemIndex = validIndexes.Count-1;
    //         }
    //         if(secondSlotItemIndex < 0) {
    //             secondSlotItemIndex = validIndexes.Count-1;
    //         }
    //         if(thirdSlotItemIndex < 0) {
    //             thirdSlotItemIndex = validIndexes.Count-1;
    //         }

    //         slots[0].sprite = inventory.toolbar[validIndexes[firstSlotItemIndex]].itemSprite;
    //         slots[1].sprite = inventory.toolbar[validIndexes[secondSlotItemIndex]].itemSprite;
    //         slots[2].sprite = inventory.toolbar[validIndexes[thirdSlotItemIndex]].itemSprite;
            
    // }
}
