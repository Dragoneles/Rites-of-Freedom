/******************************************************************************
 * 
 * File: FaceNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Node that tells the AI to change their facing direction.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using System;
using UnityEngine;

/// <summary>
/// Node that tells the AI to change their facing direction.
/// </summary>
public class FaceNode : LeafNode
{
    public enum FaceType
    {
        TowardsEnemy,
        AwayFromEnemy,
        Left,
        Right,
        Flip
    }

    [SerializeField]
    private FaceType faceType = FaceType.TowardsEnemy;

    private Character self;
    private Character target;

    private Func<bool> successCondition;

    protected override void OnInitialize()
    {
        self = Blackboard.Get(EnemyBehaviorTree.Self);
        target = Blackboard.Get(EnemyBehaviorTree.Target);
    }

    protected override void Start()
    {
        switch (faceType)
        {
            case FaceType.TowardsEnemy:
                self.FacePoint(target.Position);

                successCondition = 
                    () => self.IsFacingPoint(target.Position);
                break;

            case FaceType.AwayFromEnemy:
                if (self.IsFacingPoint(target.Position))
                    self.FaceDirection(-self.GetFacingDirection());

                successCondition = 
                    () => !self.IsFacingPoint(target.Position);
                break;

            case FaceType.Left:
                self.FaceLeft();

                successCondition = 
                    () => self.GetFacingDirection() == Direction.Left;
                break;

            case FaceType.Right:
                self.FaceRight();

                successCondition = 
                    () => self.GetFacingDirection() == Direction.Right;
                break;

            case FaceType.Flip:
                float previousDirection = self.GetFacingDirection();
                self.Flip();

                successCondition = 
                    () => self.GetFacingDirection() != previousDirection;
                break;

            default:
                break;
        }
    }

    protected override void OnReset()
    {
        successCondition = null;
    }

    protected override bool CheckNodeFailed()
    {
        return !CheckNodeSucceeded();
    }

    protected override bool CheckNodeSucceeded()
    {
        if (successCondition == null)
            return false;

        return successCondition.Invoke();
    }
}
