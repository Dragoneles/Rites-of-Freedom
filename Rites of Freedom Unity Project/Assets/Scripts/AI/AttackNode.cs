/******************************************************************************
 * 
 * File: AttackNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that tells the AI to attack.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

/// <summary>
/// Leaf node that tells the AI to attack.
/// </summary>
public class AttackNode : LeafNode
{
    protected override bool NodeFailed()
    {
        return blackboard.Get<CharacterStateMachineManager>(EnemyBehaviorTree.StateMachine).ControlsLocked;
    }

    protected override bool NodeSucceeded()
    {
        return !blackboard.Get<CharacterStateMachineManager>(EnemyBehaviorTree.StateMachine).ControlsLocked;
    }

    protected override void Start()
    {
        var stateMachine = blackboard.Get<CharacterStateMachineManager>(EnemyBehaviorTree.StateMachine);

        stateMachine.Attack();
    }
}
