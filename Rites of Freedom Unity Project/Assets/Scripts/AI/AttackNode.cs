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
using AI.BehaviorTree;

/// <summary>
/// Leaf node that tells the AI to attack.
/// </summary>
public class AttackNode : LeafNode
{
    private AIInputHandler input;
    private Character self;

    bool inputCompleted = false;

    protected override void OnInitialize()
    {
        self = Blackboard.Get(EnemyBehaviorTree.Self);
        input = Blackboard.Get(EnemyBehaviorTree.Input);
    }

    protected override void OnReset()
    {
        inputCompleted = false;
    }

    protected override void OnNodeEntered()
    {
        input.PerformAttack();

        inputCompleted = true;
    }

    protected override bool CheckNodeFailed()
    {
        return self.IsDead;
    }

    protected override bool CheckNodeSucceeded()
    {
        return inputCompleted;
    }
}
