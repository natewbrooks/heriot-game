using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public enum Tools {
        Axe,
        Hammer,   
        Scythe,
        Shovel,
        Sword,
        Water
    }


    [SerializeField] private Tools _currentTool;
    private int currentToolIndex;

    [Header("Misc")]
    private bool _combatMode;
    
    public Tools Tool { get { return _currentTool; } set { _currentTool = value; }}
    


    // Start is called before the first frame update
    void Start()
    {
        currentToolIndex = (int) _currentTool;

    }

    // Update is called once per frame
    void Update()
    {
        ScrollLogic();
    }
    
    void ScrollLogic () {
        if(!_combatMode) {
            if(Input.GetKeyDown(KeyCode.LeftBracket)) {
                // go back down the line of tools
                currentToolIndex--;
                if(currentToolIndex < 0) {
                    currentToolIndex = Enum.GetNames(typeof(Tools)).Length - 1;
                }
                _currentTool = (Tools) currentToolIndex;
            }

            if(Input.GetKeyDown(KeyCode.RightBracket)) {
                // go up the line of tools
                currentToolIndex++;
                if(currentToolIndex > Enum.GetNames(typeof(Tools)).Length - 1) {
                    currentToolIndex = 0;
                }
                _currentTool = (Tools) currentToolIndex;
            }
        }
    }
}
