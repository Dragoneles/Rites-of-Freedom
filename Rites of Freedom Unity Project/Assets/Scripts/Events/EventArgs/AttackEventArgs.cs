/******************************************************************************
 * 
 * File: AttackEventArgs.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  EventArgs for when a character is attacked.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// EventArgs for when a character is attacked.
/// </summary>
public class AttackEventArgs : EventArgs
{
    /// <summary>
    /// The attack instance that triggered the event.
    /// </summary>
    public readonly AttackInstance Attack;

    /// <summary>
    /// The character that received the attack.
    /// </summary>
    public readonly IAttackable TriggeringCharacter;

    /// <summary>
    /// The character that invoked the attack.
    /// </summary>
    public Character Attacker { get => Attack.Attacker; }

    public AttackEventArgs(AttackInstance attack, IAttackable triggeringCharacter)
    {
        Attack = attack;
        TriggeringCharacter = triggeringCharacter;
    }
}
