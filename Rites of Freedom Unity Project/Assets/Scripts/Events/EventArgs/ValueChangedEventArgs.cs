/******************************************************************************
 * 
 * File: ValueChangedEventArgs.cs
 * Author: Joseph Crump
 * Date: 2/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event arguments describing a change to a value.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// Event arguments describing a change to a value.
/// </summary>
public class ValueChangedEventArgs : EventArgs
{
    /// <summary>
    /// Value of the property before it was changed.
    /// </summary>
    public readonly float PreviousValue;

    /// <summary>
    /// Value of the property after it's changed.
    /// </summary>
    public readonly float CurrentValue;

    /// <param name="previousValue">
    /// Value of the property before it was changed.
    /// </param>
    /// <param name="currentValue">
    /// Value of the property after it's changed.
    /// </param>
    public ValueChangedEventArgs(float previousValue, float currentValue)
    {
        PreviousValue = previousValue;
        CurrentValue = currentValue;
    }
}
