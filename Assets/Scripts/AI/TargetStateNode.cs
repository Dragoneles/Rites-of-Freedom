/******************************************************************************
 * 
 * File: TargetStateNode.cs
 * Author: Joseph Crump
 * Date: 4/02/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that compares the target's state.
 *  
 ******************************************************************************/
using UnityEngine;
using AI.BehaviorTree;

/// <summary>
/// Leaf node that compares the target's state.
/// </summary>
public class TargetStateNode : LeafNode
{
    public enum StateType { Blocking, Rolling, Moving, Jumping }

    [SerializeField]
    private StateType targetState = StateType.Blocking;

    [SerializeField]
    [Tooltip(
        "Whether to treat blocking as a failure if the target " +
        "isn't facing the agent.")]
    private bool treatBlockAwayAsFailure = true;

    private Character target;
    private Character self;

    protected override void OnInitialize()
    {
        self = Blackboard.Get(EnemyBehaviorTree.Self);
    }

    protected override void OnNodeEntered()
    {
        target = Blackboard.Get(EnemyBehaviorTree.Target);
    }

    protected override bool CheckNodeFailed()
    {
        return !CheckNodeSucceeded();
    }

    protected override bool CheckNodeSucceeded()
    {
        switch (targetState)
        {
            case StateType.Blocking:
                if (!target.IsBlocking)
                    return false;

                if (treatBlockAwayAsFailure && TargetIsFacingAway())
                    return false;

                return true;

            case StateType.Rolling: return target.IsRolling;
            case StateType.Moving: return target.IsMoving;
            case StateType.Jumping: return target.IsJumping;
            default:
                break;
        }

        return false;
    }

    private bool TargetIsFacingAway()
    {
        return !target.IsFacingPoint(self.Position);
    }
}
