/******************************************************************************
 * 
 * File: AttackSequence.cs
 * Author: Joseph Crump
 * Date: 2/05/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Array of AttackInfo indexes.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Array of AttackInfo indexes.
/// </summary>
[System.Serializable]
public class AttackSequence
{
    [SerializeField]
    private AttackInfo[] Attacks = new AttackInfo[3];

    /// <summary>
    /// Generates an attack instance from an attacking character.
    /// </summary>
    public AttackInstance GetAttackFrom(Character attacker)
    {
        // REFACTOR-IN-PROGRESS
        int attackIndex = 0;

        if (attackIndex >= Attacks.Length)
            throw new System.Exception($"{attacker} {nameof(AttackSequence)} " +
                $"does not contain any info at index {attackIndex}");

        AttackInfo attack = Attacks[attackIndex];

        return new AttackInstance(attack.Damage, attacker);
    }
}
