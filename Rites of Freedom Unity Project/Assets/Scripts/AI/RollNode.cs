/******************************************************************************
 * 
 * File: RollNode.cs
 * Author: Joseph Crump
 * Date: 2/18/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that tells the AI to roll.
 *  
 ******************************************************************************/
using AI.BehaviorTree;

/// <summary>
/// Leaf node that tells the AI to jump.
/// </summary>
public class RollNode : LeafNode
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

    protected override void Start()
    {
        input.PerformRoll();

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
