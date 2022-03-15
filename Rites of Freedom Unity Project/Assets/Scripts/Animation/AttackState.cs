/******************************************************************************
 * 
 * File: AttackState.cs
 * Author: Joseph Crump
 * Date: 3/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior driving a state for a character's attack.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior driving a state for a character's attack.
/// </summary>
public class AttackState : StateMachineBehavior<Character>
{
    [SerializeField]
    [Tooltip(
        "The point in the clip where the animation holds while the " +
        "player charges the attack.")]
    private float chargePoint = 0.1f;

    private CharacterInputManager input => context.Input;
}
