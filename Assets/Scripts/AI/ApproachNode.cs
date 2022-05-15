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
using UnityEngine;

/// <summary>
/// Leaf node that instructs the AI to approach a certain distance from their
/// opponent.
/// </summary>
public class ApproachNode : LeafNode
{
    [SerializeField]
    private float targetDistance = 1f;

    private Character self;
    private Character target;
    private AIInputHandler input;

    protected override void OnInitialize()
    {
        self = Blackboard.Get(EnemyBehaviorTree.Self);
        input = Blackboard.Get(EnemyBehaviorTree.Input);
    }

    protected override void OnNodeEntered()
    {
        target = Blackboard.Get(EnemyBehaviorTree.Target);
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
