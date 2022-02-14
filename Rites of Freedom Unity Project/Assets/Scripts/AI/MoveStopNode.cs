/******************************************************************************
 * 
 * File: MoveStopNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that tells the AI to stop moving in a direction.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AI.BehaviorTree;

/// <summary>
/// Leaf node that tells the AI to start moving in a direction.
/// </summary>
public class MoveStopNode : LeafNode
{
    protected override bool NodeFailed()
    {
        return false;
    }

    protected override bool NodeSucceeded()
    {
        return true;
    }

    protected override void Start()
    {
        var stateMachine = blackboard.Get<CharacterStateMachineManager>("stateMachine");

        stateMachine.Move(0f);
    }
}
