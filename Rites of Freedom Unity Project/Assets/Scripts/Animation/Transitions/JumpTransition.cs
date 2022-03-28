/******************************************************************************
 * 
 * File: JumpTransition.cs
 * Author: Joseph Crump
 * Date: 3/23/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Character state machine behavior that allows the character to jump.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Character state machine behavior that allows the character to jump.
/// </summary>
public class JumpTransition : TransitionBehavior
{
    protected override bool GetTransitionCondition()
    {
        return Input.Jump.IsDown;
    }

    protected override void Transition(AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.Jump();
    }
}
