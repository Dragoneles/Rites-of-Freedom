/******************************************************************************
 * 
 * File: RollEventCallback.cs
 * Author: Joseph Crump
 * Date: 4/03/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Callback for a character rolling.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Callback for a character rolling.
/// </summary>
public class RollEventCallback : EventCallback<EventArgs>
{
    [SerializeField] private Character triggeringCharacter;

    protected override UnityEvent<EventArgs> Event
    {
        get => triggeringCharacter.Rolled;
    }

    protected override bool ValidateCallback(EventArgs args)
    {
        return true;
    }
}
