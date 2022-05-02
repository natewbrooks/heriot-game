using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/Item/Item", order = 0)]
public class ItemData : ScriptableObject {
    [Header("Information")]
    public string itemName;
    public Sprite itemSprite;
}
