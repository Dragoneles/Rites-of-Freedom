/******************************************************************************
 * 
 * File: DeathEventCallback.cs
 * Author: Joseph Crump
 * Date: 4/03/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Callback for when a character dies.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Callback for when a character dies.
/// </summary>
public class DeathEventCallback : EventCallback<EventArgs>
{
    [SerializeField] private Character target;

    protected override UnityEvent<EventArgs> Event
    {
        get => target.Died;
    }

    protected override bool ValidateCallback(EventArgs args)
    {
        return true;
    }
}
