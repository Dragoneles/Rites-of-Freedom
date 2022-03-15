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
public class CharacterStateMachine : StateMachineBehavior<Character>
{
    /// <summary>
    /// Whether or not the character can set animation triggers.
    /// </summary>
    public bool ShouldReadInputs { get; private set; } = true;

    protected override void OnStateInitialized(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ShouldReadInputs = true;

        context.GroundStateChanged.AddListener(OnCharacterGroundStateChanged);
    }

    protected virtual void OnCharacterGroundStateChanged(object sender, BoolEventArgs e)
    {
        SetBool(Character.AnimatorBooleans.Grounded, e);
    }
}
