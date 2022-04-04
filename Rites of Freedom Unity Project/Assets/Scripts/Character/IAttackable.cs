/******************************************************************************
 * 
 * File: IAttackable.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Interface for an object that can be attacked.
 *  
 ******************************************************************************/
using System;
using UnityEngine.Events;

/// <summary>
/// Interface for an object that can be attacked.
/// </summary>
public interface IAttackable
{
    /// <summary>
    /// Event raised when a character is attacked. Argument is attacker.
    /// </summary>
    public UnityEvent<Character> Attacked { get; }
    void ReceiveAttack(AttackInstance attack);
}
