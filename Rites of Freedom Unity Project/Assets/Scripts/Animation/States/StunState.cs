/******************************************************************************
 * 
 * File: StunState.cs
 * Author: Joseph Crump
 * Date: 3/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Animator state behavior handling a character's stun state.
 *  
 ******************************************************************************/

using UnityEngine;
/// <summary>
/// Animator state behavior handling a character's stun state.
/// </summary>
public class StunState : CharacterStateMachineBehavior
{
    [SerializeField]
    private float defaultStunDuration = 0.4f;

    protected override void OnStateEntered(AnimatorStateInfo stateInfo, int layerIndex)
    {
        var clipInfo = animator.GetCurrentAnimatorClipInfo(layerIndex);

        SetAnimationSpeedByClipLength(clipLength: clipInfo.Length);
    }

    private void SetAnimationSpeedByClipLength(float clipLength)
    {
        float duration = GetFloat(Float.StunDuration);

        // Circumvent divide by 0 by using default state length
        if (duration == 0f)
            duration = defaultStunDuration;

        float animSpeed = clipLength / duration;

        SetFloat(Float.StunAnimSpeed, animSpeed);
    }
}
