using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Body Part", menuName = "ScriptableObject/Body Part", order = 0)]
public class PartVariant : ScriptableObject {
    public string partName;
    public AnimationClip[] partAnimations;
}
