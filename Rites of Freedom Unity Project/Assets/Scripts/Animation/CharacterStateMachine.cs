/******************************************************************************
 * 
 * File: CharacterStateMachine.cs
 * Author: Joseph Crump
 * Date: 3/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Top-level behavior for the character animator. Uses input events to manage
 *  animation triggers.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Top-level behavior for the character animator. Uses input events to manage
/// animation triggers.
/// </summary>
public class CharacterStateMachine : CharacterStateMachineBehavior
{
    protected override void OnStateInitialized(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        context.GroundStateChanged.AddListener(OnCharacterGroundStateChanged);
    }

    protected virtual void OnCharacterGroundStateChanged(bool e)
    {
        SetBool(Boolean.Grounded, e);
    }
}
