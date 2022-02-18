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

    private const float Left = -1f;
    private const float Right = 1f;

    private CharacterStateMachineManager stateMachine { get; set; }
    private Character self { get; set; }
    private Character target { get; set; }

    protected override void Start()
    {
        stateMachine = blackboard.Get<CharacterStateMachineManager>(EnemyBehaviorTree.StateMachine);
        self = blackboard.Get<Character>(EnemyBehaviorTree.Self);
        target  = blackboard.Get<Character>(EnemyBehaviorTree.Target);
    }

    protected override IEnumerator Run()
    {
        switch (self.IsLeftOfPoint(target.Position))
        {
            case true:
                stateMachine.Move(Right);
                break;

            case false:
                stateMachine.Move(Left);
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
            stateMachine.StopMoving();
        }

        return inRange;
    }
}
