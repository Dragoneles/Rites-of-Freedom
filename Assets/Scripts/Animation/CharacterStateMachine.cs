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
using UnityEngine;

/// <summary>
/// Top-level behavior for the character animator. Uses input events to manage
/// animation triggers.
/// </summary>
public class CharacterStateMachine : CharacterStateMachineBehavior
{
    protected override void OnStateInitialized(AnimatorStateInfo stateInfo, int layerIndex)
    {
        SetBool(Boolean.Grounded, character.Feet.IsGrounded);

        character.GroundStateChanged.AddListener(OnCharacterGroundStateChanged);
        character.Flinched.AddListener(OnCharacterFlinch);
        character.Died.AddListener(OnCharacterDeath);
    }

    private void OnCharacterGroundStateChanged(bool e)
    {
        SetBool(Boolean.Grounded, e);
    }

    private void OnCharacterDeath(System.EventArgs e)
    {
        SetBool(Boolean.Dead, true);
    }

    private void OnCharacterFlinch(System.EventArgs e)
    {
        SetTrigger(Trigger.Flinch);
    }
}
