/******************************************************************************
 * 
 * File: LightAttackTransition.cs
 * Author: Joseph Crump
 * Date: 3/23/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Character state machine behavior that allows the character to transition
 *  to a light attack.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Character state machine behavior that allows the character to transition
/// to a light attack.
/// </summary>
public class LightAttackTransition : TransitionBehavior
{
    protected override bool GetTransitionCondition()
    {
        return Input.Attack.WasPressed;
    }

    protected override void Transition(AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetTrigger(Trigger.LightAttack);
    }
}
