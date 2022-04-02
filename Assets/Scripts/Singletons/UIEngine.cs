using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEngine : Singleton<UIEngine>
{
    [SerializeField] Equipment equipment;
    
    [Header("Equipment UI")]
    [SerializeField] private TMP_Text leftHandText;
    [SerializeField] private TMP_Text rightHandText;
    [SerializeField] private TMP_Text combatModeText;
    [SerializeField] private TMP_Text toolText;


    private void Start() {
        UpdateAllUI();
    }
    
    public void UpdateAllUI() {
        UpdateLefthandUI(equipment.LeftHand);
        UpdateRighthandUI(equipment.RightHand);
        UpdateCombatUI(equipment.CombatMode);
        UpdateToolUI(equipment.Tool);
    }

    public void UpdateLefthandUI(Equipment.Arsenal weapon) {
        leftHandText.text = "Left Hand: " + weapon.ToString();
    }
    
    public void UpdateRighthandUI(Equipment.Arsenal weapon) {
        rightHandText.text = "Right Hand: " + weapon.ToString();
    }

    public void UpdateToolUI(Equipment.Tools tool) {
        toolText.text = "Tool: " + tool.ToString();
    }
    
    public void UpdateCombatUI(bool yaynay) {
        combatModeText.text = "Combat Mode: " + yaynay;
    }

}
