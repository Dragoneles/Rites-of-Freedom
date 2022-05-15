/******************************************************************************
 * 
 * File: AttackInstance.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Instance of an attack from a character.
 *  
 ******************************************************************************/

/// <summary>
/// Instance of an attack from a character.
/// </summary>
public class AttackInstance
{
    /// <summary>
    /// Amount of damage dealt by the attack.
    /// </summary>
    public readonly float Damage;

    public readonly float StunDuration;

    /// <summary>
    /// The character invoking the attack.
    /// </summary>
    public readonly Character Attacker;

    public AttackInstance(float damage, float stunDuration, Character attacker)
    {
        Damage = damage;
        StunDuration = stunDuration;
        Attacker = attacker;
    }
}
