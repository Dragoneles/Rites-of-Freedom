/******************************************************************************
 * 
 * File: TargetAliveNode.cs
 * Author: Joseph Crump
 * Date: 2/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior tree node that succeeds if the target is alive.
 *  
 ******************************************************************************/
using AI.BehaviorTree;

/// <summary>
/// Behavior tree node that succeeds if the target is alive.
/// </summary>
public class TargetAliveNode : LeafNode
{
    protected override bool CheckNodeFailed()
    {
        Character target = Blackboard.Get(EnemyBehaviorTree.Target);

        return target.IsDead;
    }

    protected override bool CheckNodeSucceeded()
    {
        Character enemy = Blackboard.Get(EnemyBehaviorTree.Target);

        return !enemy.IsDead;
    }
}
