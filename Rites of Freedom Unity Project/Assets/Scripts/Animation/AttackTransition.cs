/******************************************************************************
 * 
 * File: AttackTransition.cs
 * Author: Joseph Crump
 * Date: 3/23/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Character state machine behavior that allows the character to attack.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character state machine behavior that allows the character to attack.
/// </summary>
public class AttackTransition : TransitionBehavior
{
    protected override bool GetTransitionCondition()
    {
        return Input.Attack.WasPressed;
    }

    protected override void Transition(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetTrigger(Trigger.StartAttack);
    }
}
