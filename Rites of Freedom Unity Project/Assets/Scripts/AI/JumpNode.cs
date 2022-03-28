/******************************************************************************
 * 
 * File: JumpNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that tells the AI to jump.
 *  
 ******************************************************************************/
using AI.BehaviorTree;

/// <summary>
/// Leaf node that tells the AI to jump.
/// </summary>
public class JumpNode : LeafNode
{
    private AIInputHandler input { get; set; }

    protected override void OnInitialize()
    {
        input = blackboard.Get(EnemyBehaviorTree.Input);
    }

    protected override void Start()
    {
        input.PerformJump();
    }

    protected override bool CheckNodeFailed()
    {
        return false;
    }

    protected override bool CheckNodeSucceeded()
    {
        return true;
    }
}
