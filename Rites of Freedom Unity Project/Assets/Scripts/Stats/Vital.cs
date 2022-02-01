/******************************************************************************
 * 
 * File: Vital.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Type of stat that can be depleted.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type of stat that can be depleted.
/// </summary>
[System.Serializable]
public class Vital : Stat
{
    [SerializeField]
    protected int currentValue = 0;

    public override int Value => currentValue;
    public int MaxValue => base.Value;

    public Vital() { }
    public Vital(int maxValue) : base(maxValue) 
    {
        currentValue = maxValue;
    }

    public override void SetValue(int value)
    {
        currentValue = Mathf.Clamp(value, 0, MaxValue);
    }

    public static implicit operator int(Vital vital)
    {
        return vital.Value;
    }
}
