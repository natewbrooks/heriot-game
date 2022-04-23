using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {Axe, Sword, Polearm, Empty};

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObject/Item/Weapon", order = 0)]
public class WeaponData : ItemData {
    
    public WeaponType type;

    [Header("Damage")]
    public Vector2 damageRange;
    public int knockback;
    [Range(1,30)]
    public int speed;

    [Header("Visuals")]
    public Sprite sprite;
    public AnimationClip attackAnimation;
    // public Animation anim;
}