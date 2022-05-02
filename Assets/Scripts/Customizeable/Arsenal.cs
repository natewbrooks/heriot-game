using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arsenal : MonoBehaviour {
    public ItemData activeLeft;
    public ItemData activeRight;

    public ItemData[] leftWeapons = new ItemData[2];
    public ItemData[] rightWeapons = new ItemData[2];

    private void Start() {
        activeLeft = leftWeapons[0];
        activeRight = rightWeapons[0];

    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.LeftBracket)) {
            if(activeLeft == leftWeapons[0]) {
                activeLeft = leftWeapons[1];
            } else {
                activeLeft = leftWeapons[0];
            }
        }


        if(Input.GetKeyDown(KeyCode.RightBracket)) {
            if(activeRight == rightWeapons[0]) {
                activeRight = rightWeapons[1];
            } else {
                activeRight = rightWeapons[0];
            }
        }
    }
}