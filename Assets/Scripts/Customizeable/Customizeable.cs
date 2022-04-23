using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnityEngine;

public class Customizeable : MonoBehaviour
{
    [Header("Active Styles")]
    public PartVariant hair;
    public PartVariant beard;
    public PartVariant chest;

    [Header("Options")]
    public PartVariant[] hairStyles;
    public PartVariant[] beardStyles;
    public PartVariant[] chestStyles;

    private Animator animator;
    private AnimatorOverrideController animatorOverride;
    private string currentLayer = " ";

    private void Start() {
        animator = GetComponent<Animator>();
        animatorOverride = new AnimatorOverrideController(animator.runtimeAnimatorController);

        hair = hairStyles[0];
        Swap(hairStyles, "Curly Hair");

    }

    public void Swap(PartVariant[] styleArray, string partName) {
        FindLayer(partName);
        foreach (PartVariant part in styleArray) {
            if(part.partName == partName) {
                FieldInfo field = this.GetType().GetField(currentLayer.ToLower());
                field.SetValue(this, part);
                SetAnimations(part);
                break;
            }
        }
        currentLayer = " ";
    }

    public void Cycle(string layer) {
        currentLayer = layer;
        FieldInfo array = this.GetType().GetField(layer.ToLower()+"Styles");
        FieldInfo field = this.GetType().GetField(layer.ToLower());

        PartVariant[] styleOptions = (PartVariant[]) array.GetValue(this);

        int index = Array.IndexOf(styleOptions, field.GetValue(this));
        if(index+1 >= styleOptions.Length) {
            field.SetValue(this, styleOptions[0]);
            SetAnimations(styleOptions[0]);
            return;
        }
        field.SetValue(this, styleOptions[index+1]);
        SetAnimations(styleOptions[index+1]);
        currentLayer = " ";
    }

    public void SetAnimations(PartVariant newPart) {
        animatorOverride[currentLayer + " Idle"] = newPart.partAnimations[0]; 
        animator.runtimeAnimatorController = animatorOverride;
    }

    public void FindLayer(string proposed) {
        if(proposed.Contains("Hair")) {
            currentLayer = "Hair";
        } else if (proposed.Contains("Beard")) {
            currentLayer = "Beard";
        } else if (proposed.Contains("Head")) {
            currentLayer = "Head";
        } else if (proposed.Contains("Chest")) {
            currentLayer = "Chest";
        }
    }

}
