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

/// <summary>
/// Node that tells the AI to face their opponent.
/// </summary>
public class FaceEnemyNode : LeafNode
{
    private Character self { get; set; }
    private Character target { get; set; }

    protected override void OnInitialize()
    {
        self = blackboard.Get<Character>(EnemyBehaviorTree.Self);
        target = blackboard.Get<Character>(EnemyBehaviorTree.Target);
    }

    protected override void Start()
    {
        self.FacePoint(target.Position);
    }

    protected override bool CheckNodeFailed()
    {
        return !self.IsFacingPoint(target.Position);
    }

    protected override bool CheckNodeSucceeded()
    {
        return self.IsFacingPoint(target.Position);
    }
}
