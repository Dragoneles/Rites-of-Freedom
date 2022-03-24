/******************************************************************************
 * 
 * File: RollTransition.cs
 * Author: Joseph Crump
 * Date: 3/23/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Character state machine behavior that allows the character to roll.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Character state machine behavior that allows the character to roll.
/// </summary>
public class RollTransition : TransitionBehavior
{
    protected override bool GetTransitionCondition()
    {
        return Input.Roll.WasReleased;
    }

    protected override void Transition(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float moveDirection = Input.MovementAxis.Value;

        SetTrigger(Trigger.Roll);

        character.Roll(moveDirection);
    }
}
