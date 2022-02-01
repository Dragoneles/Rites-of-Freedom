/******************************************************************************
 * 
 * File: FloatEventArgs.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  EventArguments that can be implicitly converted to a float value.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// EventArguments that can be implicitly converted to a float value.
/// </summary>
public class FloatEventArgs : EventArgs
{
    public readonly float Value;

    public FloatEventArgs(float value)
    {
        Value = value;
    }

    public static implicit operator float(FloatEventArgs e)
    {
        return e.Value;
    }

    public static implicit operator FloatEventArgs(float value)
    {
        return new FloatEventArgs(value);
    }
}
