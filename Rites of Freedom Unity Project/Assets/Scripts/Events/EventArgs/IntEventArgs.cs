/******************************************************************************
 * 
 * File: IntEventArgs.cs
 * Author: Joseph Crump
 * Date: 2/04/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  EventArguments that can be implicitly converted to an integer value.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// EventArguments that can be implicitly converted to an integer value.
/// </summary>
public class IntEventArgs : EventArgs
{
    public readonly int Value;

    public IntEventArgs(int value)
    {
        Value = value;
    }

    public static implicit operator int(IntEventArgs e)
    {
        return e.Value;
    }

    public static implicit operator IntEventArgs(int value)
    {
        return new IntEventArgs(value);
    }
}
