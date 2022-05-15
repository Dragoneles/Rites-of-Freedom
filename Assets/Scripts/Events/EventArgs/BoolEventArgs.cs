/******************************************************************************
 * 
 * File: BoolEventArgs.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  EventArguments that can be implicitly converted to a boolean value.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// EventArguments that can be implicitly converted to a boolean value.
/// </summary>
public class BoolEventArgs : EventArgs
{
    public readonly bool Value;

    public BoolEventArgs(bool value)
    {
        Value = value;
    }

    public static implicit operator bool(BoolEventArgs e)
    {
        return e.Value;
    }

    public static implicit operator BoolEventArgs(bool value)
    {
        return new BoolEventArgs(value);
    }
}
