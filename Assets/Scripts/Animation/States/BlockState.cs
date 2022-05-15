/******************************************************************************
 * 
 * File: BlockState.cs
 * Author: Joseph Crump
 * Date: 3/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that controls a character's Block flag.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Behavior that controls a character's Block flag.
/// </summary>
public class BlockState : CharacterStateMachineBehavior
{
    protected override void OnStateEntered(AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool blockState = GetBool(Boolean.Blocking);
        character.SetBlockState(blockState);

        character.Blocked.AddListener(SetBlockTrigger);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool blockState = GetBool(Boolean.Blocking);
        character.SetBlockState(blockState);

        character.Blocked.RemoveListener(SetBlockTrigger);
    }

    private void SetBlockTrigger(AttackEventArgs e)
    {
        SetTrigger(Trigger.Block);
    }
}
