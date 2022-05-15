/******************************************************************************
 * 
 * File: TransitionBehavior.cs
 * Author: Joseph Crump
 * Date: 3/23/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Abstract class for a state behavior that allows that state to transition
 *  to a different state.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Abstract class for a state behavior that allows that state to transition
/// to a different state.
/// </summary>
public abstract class TransitionBehavior : CharacterStateMachineBehavior
{
    [SerializeField]
    [Tooltip("Portion of the animation that needs to be completed before a " +
        "transition is allowed.")]
    private float stateProgressRequirement = 0f;
    private bool inputCached { get; set; } = false;
    private bool transitioning { get; set; } = false;

    protected override void OnStateEntered(AnimatorStateInfo stateInfo, int layerIndex)
    {
        inputCached = false;
        transitioning = false;
    }

    public override sealed void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (transitioning)
            return;

        if (stateInfo.normalizedTime < stateProgressRequirement)
        {
            TryCacheInput();
            return;
        }

        if (inputCached || GetTransitionCondition())
        {
            transitioning = true;

            Transition(stateInfo, layerIndex);
        }
    }

    private void TryCacheInput()
    {
        if (inputCached)
            return;

        inputCached = GetTransitionCondition();
    }

    protected abstract bool GetTransitionCondition();

    protected abstract void Transition(AnimatorStateInfo stateInfo, int layerIndex);
}
