using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shield", menuName = "ScriptableObject/Item/Shield", order = 0)]
public class ShieldData : ItemData {
   
    [Header("Stats")]
    [Range(0,2000)]
    public int health;

    [Header("Visuals")]
    public Sprite sprite;
    // public Animation anim;
}