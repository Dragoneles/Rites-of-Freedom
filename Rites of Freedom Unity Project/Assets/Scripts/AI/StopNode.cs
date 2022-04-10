/******************************************************************************
 * 
 * File: StopNode.cs
 * Author: Joseph Crump
 * Date: 4/09/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that instructs the AI to stop moving.
 *  
 ******************************************************************************/
using AI.BehaviorTree;

/// <summary>
/// Leaf node that instructs the AI to stop moving.
/// </summary>
public class StopNode : LeafNode
{
    private Character self;
    private AIInputHandler input;

    protected override void OnNodeEntered()
    {
        self = Blackboard.Get(EnemyBehaviorTree.Self);
        input = Blackboard.Get(EnemyBehaviorTree.Input);

        input.PerformMove(0f);
    }

    protected override bool CheckNodeFailed()
    {
        return self.IsDead;
    }

    protected override bool CheckNodeSucceeded()
    {
        return !self.IsDead;
    }
}
