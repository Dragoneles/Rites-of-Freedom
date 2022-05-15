/******************************************************************************
 * 
 * File: AttackState.cs
 * Author: Joseph Crump
 * Date: 3/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Contains state info for a character attack animation.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Contains state info for a character attack animation.
/// </summary>
public class AttackState : CharacterStateMachineBehavior
{
    [SerializeField]
    [Tooltip("Details about the attack.")]
    private AttackInfo attackInfo;

    protected override void OnStateEntered(AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.LastAttack = attackInfo;
    }
}
