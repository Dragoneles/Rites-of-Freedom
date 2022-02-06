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
    [Tooltip("Current value of this vital.")]
    protected int currentValue = 0;

    public override int Value => currentValue;
    public int MaxValue => base.Value;

    public Vital() { }
    public Vital(int maxValue) : base(maxValue) 
    {
        currentValue = maxValue;
    }

    /// <summary>
    /// Add to the vital's current value.
    /// </summary>
    public void Add(int value)
    {
        SetValue(currentValue + value);
    }

    /// <summary>
    /// Subtract from the vital's current value.
    /// </summary>
    public void Subtract(int value)
    {
        Add(-value);
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
