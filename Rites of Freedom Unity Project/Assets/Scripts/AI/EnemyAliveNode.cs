/******************************************************************************
 * 
 * File: EnemyAliveNode.cs
 * Author: Joseph Crump
 * Date: 2/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior tree node that succeeds if the opponent is alive.
 *  
 ******************************************************************************/
using AI.BehaviorTree;

/// <summary>
/// Behavior tree node that succeeds if the opponent is alive.
/// </summary>
public class EnemyAliveNode : LeafNode
{
    protected override bool CheckNodeFailed()
    {
        Character enemy = blackboard.Get<Character>(EnemyBehaviorTree.Target);

        return enemy.IsDead;
    }

    protected override bool CheckNodeSucceeded()
    {
        Character enemy = blackboard.Get<Character>(EnemyBehaviorTree.Target);

        return !enemy.IsDead;
    }
}
