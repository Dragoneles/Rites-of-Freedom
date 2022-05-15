/******************************************************************************
 * 
 * File: StateEnterEvent.cs
 * Author: Joseph Crump
 * Date: 3/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Serialized event triggered when a state is entered.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Serialized event triggered when a state is entered.
/// </summary>
public class StateEnterEvent : CharacterStateMachineBehavior
{
    [SerializeField]
    private UnityEvent onStateEntered = new UnityEvent();

    protected override void OnStateEntered(AnimatorStateInfo stateInfo, int layerIndex)
    {
        onStateEntered?.Invoke();
    }
}
