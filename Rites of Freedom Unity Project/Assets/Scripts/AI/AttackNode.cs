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
    private AIInputHandler input { get; set; }

    protected override void OnInitialize()
    {
        input = blackboard.Get(EnemyBehaviorTree.Input);
    }

    protected override void Start()
    {
        input.PerformAttack();
    }

    protected override bool CheckNodeFailed()
    {
        return !CheckNodeSucceeded();
    }

    protected override bool CheckNodeSucceeded()
    {
        return true;
    }
}
