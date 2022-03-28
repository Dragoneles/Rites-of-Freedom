/******************************************************************************
 * 
 * File: ApproachNode.cs
 * Author: Joseph Crump
 * Date: 2/15/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that instructs the AI to approach a certain distance from their
 *  opponent.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Leaf node that instructs the AI to approach a certain distance from their
/// opponent.
/// </summary>
public class ApproachNode : LeafNode
{
    [SerializeField]
    private float targetDistance = 1f;

    private Character self { get; set; }
    private Character target { get; set; }
    private AIInputHandler input { get; set; }

    protected override void Start()
    {
        self = blackboard.Get(EnemyBehaviorTree.Self);
        target  = blackboard.Get(EnemyBehaviorTree.Target);
        input  = blackboard.Get(EnemyBehaviorTree.Input);
    }

    protected override IEnumerator Run()
    {
        switch (self.IsLeftOfPoint(target.Position))
        {
            case true:
                input.PerformMove(Direction.Right);
                break;

            case false:
                input.PerformMove(Direction.Left);
                break;
        }

        yield return new WaitForEndOfFrame();
    }

    protected override bool CheckNodeFailed()
    {
        return self.IsDead;
    }

    protected override bool CheckNodeSucceeded()
    {
        bool inRange = Vector2.Distance(self.Position, target.Position) <= targetDistance;
        if (inRange)
        {
            input.PerformMove(0f);
        }

        return inRange;
    }
}
