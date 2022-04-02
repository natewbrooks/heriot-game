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

    public enum Arsenal {
        Axe,
        Bow,
        Hammer,
        Mace,
        Scythe, 
        Shield, 
        Sword, 
        Empty
    }

    [Header("Tool Settings")]
    [SerializeField] private Tools _currentTool;
    private int currentToolIndex;

    [Header("Equipment Settings")]
    [SerializeField] private Arsenal[] weaponsEquipped = {Arsenal.Empty, Arsenal.Empty, Arsenal.Empty, Arsenal.Empty};

    [SerializeField] private Arsenal _leftHand;
    [SerializeField] private Arsenal _rightHand;

    [Header("Misc")]
    [SerializeField] private bool _dualWielding;
    [SerializeField] private bool _combatMode;
    
    public Arsenal LeftHand { get { return _leftHand; } }
    public Arsenal RightHand { get { return _rightHand; } }
    public Tools Tool { get { return _currentTool; } }
    public bool CombatMode { get { return _combatMode; } }

    


    // Start is called before the first frame update
    void Start()
    {
        _rightHand = weaponsEquipped[0];
        _leftHand = weaponsEquipped[2];
        currentToolIndex = (int) _currentTool;

        UIEngine.Instance.UpdateAllUI();
    }

    // Update is called once per frame
    void Update()
    {
        ScrollLogic();
    }



    void ScrollLogic () {
        
        // toggle between combat and non combat modes to swap and use weapons
        if(Input.GetKeyDown(KeyCode.Tab)) {
            _combatMode = !_combatMode;
            UIEngine.Instance.UpdateCombatUI(_combatMode);
        }

        
        if(_combatMode) {
            // left hand scrolls between first and second equipped weapon
            if(Input.GetKeyDown(KeyCode.LeftBracket)) {
                if(_leftHand == weaponsEquipped[2]) {
                    _leftHand = weaponsEquipped[3];
                } else {
                    _leftHand = weaponsEquipped[2];
                }
                UIEngine.Instance.UpdateLefthandUI(_leftHand);
            }

            // right hand scrolls between the third and fourth equipped weapon
            if(Input.GetKeyDown(KeyCode.RightBracket)) {
                if(_rightHand == weaponsEquipped[0]) {
                    _rightHand = weaponsEquipped[1];
                } else {
                    _rightHand = weaponsEquipped[0];
                }
                UIEngine.Instance.UpdateRighthandUI(_rightHand);
            }

            if(_rightHand == _leftHand) {
                _dualWielding = true;
            } else {
                _dualWielding = false;
            }
        } else {
            if(Input.GetKeyDown(KeyCode.LeftBracket)) {
                // go back down the line of tools
                currentToolIndex--;
                if(currentToolIndex < 0) {
                    currentToolIndex = Enum.GetNames(typeof(Tools)).Length - 1;
                }
                _currentTool = (Tools) currentToolIndex;
                UIEngine.Instance.UpdateToolUI(_currentTool);
            }

            if(Input.GetKeyDown(KeyCode.RightBracket)) {
                // go up the line of tools
                currentToolIndex++;
                if(currentToolIndex > Enum.GetNames(typeof(Tools)).Length - 1) {
                    currentToolIndex = 0;
                }
                _currentTool = (Tools) currentToolIndex;
                UIEngine.Instance.UpdateToolUI(_currentTool);
            }
        }
    }
}
