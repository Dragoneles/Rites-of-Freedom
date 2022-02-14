/******************************************************************************
 * 
 * File: MoveStartNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that tells the AI to start moving in a direction.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

/// <summary>
/// Leaf node that tells the AI to start moving in a direction.
/// </summary>
public class MoveStartNode : LeafNode
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
        var target = blackboard.Get<Character>(EnemyBehaviorTree.Target);
        bool targetIsToTheLeft = target.transform.position.x < transform.position.x;

        var stateMachine = blackboard.Get<CharacterStateMachineManager>(EnemyBehaviorTree.StateMachine);

        float direction = targetIsToTheLeft ? -1f : 1f;

        stateMachine.Move(direction);
    }
}
