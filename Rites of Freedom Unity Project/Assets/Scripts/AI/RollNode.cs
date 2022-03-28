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
    private Character self { get; set; }
    private AIInputHandler input { get; set; }

    protected override void OnInitialize()
    {
        self = blackboard.Get(EnemyBehaviorTree.Self);
        input = blackboard.Get(EnemyBehaviorTree.Input);
    }

    protected override void Start()
    {
        input.PerformRoll();
    }

    protected override bool CheckNodeFailed()
    {
        return !CheckNodeSucceeded();
    }

    protected override bool CheckNodeSucceeded()
    {
        return self.IsRolling;
    }
}
