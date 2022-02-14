/******************************************************************************
 * 
 * File: FaceEnemyNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Node that tells the AI to face their opponent.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node that tells the AI to face their opponent.
/// </summary>
public class FaceEnemyNode : LeafNode
{
    protected override bool NodeFailed()
    {
        return !IsFacingTarget();
    }

    protected override bool NodeSucceeded()
    {
        return IsFacingTarget();
    }

    protected override void Start()
    {
        var self = blackboard.Get<Character>(EnemyBehaviorTree.Self);

        if (IsFacingTarget() == false)
            self.Flip();
    }

    private bool IsFacingTarget()
    {
        var enemy = blackboard.Get<Character>(EnemyBehaviorTree.Target);
        var self = blackboard.Get<Character>(EnemyBehaviorTree.Self);

        return (self.IsFacingPoint(enemy.transform.position));
    }
}
