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
        Dagger,
        Javelin,
        Mace,
        Shield, 
        Spear,
        Sword, 
        ThrowingAxe,
        Empty
    }

    [Header("Tool Settings")]
    [SerializeField] private Tools _currentTool;
    private int currentToolIndex;

    [Header("Equipment Settings")]
    [SerializeField] private GameObject menu;
    [SerializeField] public Arsenal[] weaponsEquipped = {Arsenal.Empty, Arsenal.Empty, Arsenal.Empty, Arsenal.Empty};

    [SerializeField] private Arsenal _leftHand;
    [SerializeField] private Arsenal _rightHand;
    private int _leftHandIndex;
    private int _rightHandIndex;

    [Header("Misc")]
    [SerializeField] private bool _combatMode;
    
    public Arsenal LeftHand { get { return _leftHand; } set { _leftHand = value; } }
    public Arsenal RightHand { get { return _rightHand; } set { _rightHand = value; } }
    public Tools Tool { get { return _currentTool; } set { _currentTool = value; }}
    public bool CombatMode { get { return _combatMode; } }

    public int LeftHandIndex {get { return _leftHandIndex; } }
    public int RightHandIndex {get { return _rightHandIndex; } }

    


    // Start is called before the first frame update
    void Start()
    {
        _leftHand = weaponsEquipped[0];
        _rightHand = weaponsEquipped[2];
        _leftHandIndex = 0;
        _rightHandIndex = 2;

        currentToolIndex = (int) _currentTool;

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
        }

        
        if(_combatMode) {
            // left hand scrolls between first and second equipped weapon
            if(Input.GetKeyDown(KeyCode.LeftBracket)) {
                if(_leftHand == weaponsEquipped[0]) {
                    _leftHandIndex = 1;
                    _leftHand = weaponsEquipped[1];
                } else {
                    _leftHandIndex = 0;
                    _leftHand = weaponsEquipped[0];
                }
            }

            // right hand scrolls between the third and fourth equipped weapon
            if(Input.GetKeyDown(KeyCode.RightBracket)) {
                if(_rightHand == weaponsEquipped[2]) {
                    _rightHandIndex = 3;
                    _rightHand = weaponsEquipped[3];
                } else {
                    _rightHandIndex = 2;
                    _rightHand = weaponsEquipped[2];
                }
            }
        } else {
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
