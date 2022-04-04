/******************************************************************************
 * 
 * File: AttackEventCallback.cs
 * Author: Joseph Crump
 * Date: 4/03/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Callback for a character attacking an attackable object.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Callback for a character attacking an attackable object.
/// </summary>
public abstract class AttackEventCallback<TAttackable> : EventCallback<Character>
    where TAttackable : IAttackable
{
    [SerializeField] private Character triggeringCharacter;
    [SerializeField] private TAttackable target;

    protected override UnityEvent<Character> Event
    {
        get => target.Attacked;
    }

    protected override bool ValidateCallback(Character attacker)
    {
        return (attacker == triggeringCharacter);
    }
}
