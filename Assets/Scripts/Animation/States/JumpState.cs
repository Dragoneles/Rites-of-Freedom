/******************************************************************************
 * 
 * File: JumpState.cs
 * Author: Joseph Crump
 * Date: 3/14/22
 * 
 * Copyright ? 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior driving a state for a character's jump.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Behavior driving a state for a character's jump.
/// </summary>
public class JumpState : CharacterStateMachineBehavior
{
    [SerializeField]
    private float lowerYVelocityLimit = -3f;

    [SerializeField]
    private float upperYVelocityLimit = 3f;

    protected override void OnStateEntered(AnimatorStateInfo stateInfor, int layerIndex)
    {
        context.YVelocityChanged.AddListener(OnCharacterYVelocityChanged);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        context.YVelocityChanged.RemoveListener(OnCharacterYVelocityChanged);
    }

    protected virtual void OnCharacterYVelocityChanged(float e)
    {
        float blendValue = Mathf.InverseLerp(lowerYVelocityLimit, upperYVelocityLimit, e);
        SetFloat(Float.JumpDirection, blendValue);
    }
}
