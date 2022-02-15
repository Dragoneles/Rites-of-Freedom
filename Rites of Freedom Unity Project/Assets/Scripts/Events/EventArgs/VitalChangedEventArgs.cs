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

/// <summary>
/// Event arguments describing a change to a value.
/// </summary>
public class VitalChangedEventArgs : ValueChangedEventArgs
{
    /// <summary>
    /// The vital that was changed.
    /// </summary>
    public readonly Vital Vital;

    public VitalChangedEventArgs(Vital vital, float previousValue, float currentValue)
        : base(previousValue, currentValue)
    {
        Vital = vital;
    }
}
