/******************************************************************************
 * 
 * File: BlockEventCallback.cs
 * Author: Joseph Crump
 * Date: 4/03/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Callback for a character blocking.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Callback for a character blocking.
/// </summary>
public class BlockEventCallback : EventCallback<AttackEventArgs>
{
    [SerializeField] private Character blocker;
    [SerializeField] private Character attacker;

    protected override UnityEvent<AttackEventArgs> Event
    { 
        get => blocker.Blocked;
    }

    protected override bool ValidateCallback(AttackEventArgs args)
    {
        return (args.Attacker == attacker);
    }
}
