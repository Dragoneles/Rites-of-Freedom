/******************************************************************************
 * 
 * File: StateExitEvent.cs
 * Author: Joseph Crump
 * Date: 3/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Serialized event triggered when a state is exited.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

/// <summary>
/// Serialized event triggered when a state is entered.
/// </summary>
public class StateExitEvent : CharacterStateMachineBehavior
{
    [SerializeField]
    private UnityEvent onStateExited = new UnityEvent();

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateExited?.Invoke();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
    {
        OnStateExit(animator, stateInfo, layerIndex);
    }
}
