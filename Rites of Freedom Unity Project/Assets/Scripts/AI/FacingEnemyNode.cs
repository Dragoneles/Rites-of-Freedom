/******************************************************************************
 * 
 * File: FacingEnemyNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Node that checks whether the AI is facing their opponent.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node that checks whether the AI is facing their opponent.
/// </summary>
public class FacingEnemyNode : LeafNode
{
    protected override bool NodeFailed()
    {
        return !IsFacingTarget();
    }

    protected override bool NodeSucceeded()
    {
        return IsFacingTarget();
    }

    private bool IsFacingTarget()
    {
        var enemy = blackboard.Get<Character>(EnemyBehaviorTree.Target);
        var self = blackboard.Get<Character>(EnemyBehaviorTree.Self);

        return (self.IsFacingPoint(enemy.transform.position));
    }
}
