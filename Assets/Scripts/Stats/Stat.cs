/******************************************************************************
 * 
 * File: Stat.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Value unit used for Character attributes.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Value unit used for Character attributes.
/// </summary>
[System.Serializable]
public class Stat
{
    [SerializeField]
    private float value = 0;
    public virtual float Value { get => value; }

    public Stat() { }
    public Stat(float value)
    {
        this.value = value;
    }

    public virtual void SetValue(float value)
    {
        this.value = value;
    }

    public static implicit operator float(Stat stat)
    {
        return stat.Value;
    }
}
