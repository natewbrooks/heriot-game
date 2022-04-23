using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crop", menuName = "ScriptableObject/Crop", order = 0)]
public class CropData : ItemData {

    [Header("Stats")]
    public int water = 50;
    public float matureTimeSeconds;

    [Header("Visuals")]
    public List<Sprite> stageSprites = new List<Sprite>();
    public Sprite rottenSprite;
    public GameObject harvestCrop;

}
