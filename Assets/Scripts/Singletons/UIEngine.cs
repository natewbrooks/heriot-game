using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIEngine : Singleton<UIEngine>
{
    [SerializeField] Equipment equipment;
    [SerializeField] private GameObject equipmentMenu;
    private bool equipmentMenuActive;

    [SerializeField] public Sprite[] equipmentIcons = new Sprite[Enum.GetNames(typeof(Equipment.Arsenal)).Length];

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            equipmentMenuActive = !equipmentMenuActive;
            equipmentMenu.GetComponent<EquipmentMenu>().CloseSlots();
            equipmentMenu.SetActive(equipmentMenuActive);
            GameEngine.Instance.Player.Frozen = equipmentMenuActive;
        }
    }
}
