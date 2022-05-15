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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type of stat that can be depleted.
/// </summary>
[Serializable]
public class Vital : Stat
{
    public event EventHandler<ValueChangedEventArgs> ValueChanged;

    [SerializeField]
    [Tooltip("Current value of this vital.")]
    protected float currentValue = 0;

    public override float Value => currentValue;
    public float MaxValue => base.Value;

    public Vital() { }
    public Vital(float maxValue) : base(maxValue) 
    {
        currentValue = maxValue;
    }

    /// <summary>
    /// Add to the vital's current value.
    /// </summary>
    public void Add(float value)
    {
        SetValue(currentValue + value);
    }

    /// <summary>
    /// Subtract from the vital's current value.
    /// </summary>
    public void Subtract(float value)
    {
        Add(-value);
    }

    public override void SetValue(float value)
    {
        float previousValue = this.Value;

        currentValue = Mathf.Clamp(value, 0, MaxValue);

        float newValue = this.Value;

        if (previousValue != newValue)
        {
            var eventArgs = new ValueChangedEventArgs(previousValue, newValue);
            ValueChanged?.Invoke(this, eventArgs);
        }
    }

    /// <summary>
    /// Set the maximum value of this vital.
    /// </summary>
    /// <param name="setCurrentValue">
    /// If true, also sets the current value to match the new maximum value.
    /// </param>
    public void SetMaxValue(float value, bool setCurrentValue = false)
    {
        base.SetValue(value);

        if (setCurrentValue)
            SetValue(value);
    }

    public static implicit operator float(Vital vital)
    {
        return vital.Value;
    }
}
