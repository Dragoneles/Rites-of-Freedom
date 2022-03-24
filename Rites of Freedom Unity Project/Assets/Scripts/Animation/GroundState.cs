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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// State behavior for the Ground state of the controller's animator.
/// </summary>
public class GroundState : CharacterStateMachineBehavior
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TryMove();
    }

    private void TryMove()
    {
        float moveDirection = Input.MovementAxis.Value;

        bool isMoving = moveDirection != 0f;

        SetBool(Boolean.Moving, isMoving);

        if (!isMoving)
        {
            character.Stop();
            return;
        }

        character.Move(moveDirection);
        SetFloat(Float.MoveAnimSpeed, Mathf.Abs(moveDirection));
    }
}
