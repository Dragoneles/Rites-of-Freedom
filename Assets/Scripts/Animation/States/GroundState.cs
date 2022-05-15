/******************************************************************************
 * 
 * File: GroundState.cs
 * Author: Joseph Crump
 * Date: 3/23/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  State behavior for the Ground state of the controller's animator.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// State behavior for the Ground state of the controller's animator.
/// </summary>
public class GroundState : CharacterStateMachineBehavior
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (TryBlock())
            return;

        if (TryMove())
            return;
    }

    private bool TryMove()
    {
        float moveDirection = Input.MovementAxis.Value;

        bool isMoving = moveDirection != 0f;

        SetBool(Boolean.Moving, isMoving);

        return GetBool(Boolean.Moving);
    }

    private bool TryBlock()
    {
        SetBool(Boolean.Blocking, Input.Block.IsDown);

        return GetBool(Boolean.Blocking);
    }
}
