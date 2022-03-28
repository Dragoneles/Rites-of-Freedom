/******************************************************************************
 * 
 * File: MoveState.cs
 * Author: Joseph Crump
 * Date: 3/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  State behavior that allows a character to move.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// State behavior that allows a character to move.
/// </summary>
public class MoveState : CharacterStateMachineBehavior
{
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Move();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        character.Stop();
    }

    private void Move()
    {
        float moveDirection = Input.MovementAxis.Value;

        character.Move(moveDirection);

        SetFloat(Float.MoveAnimSpeed, Mathf.Abs(moveDirection));
    }
}
