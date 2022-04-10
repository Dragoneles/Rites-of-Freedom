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

    private Character target;

    protected override void OnInitialize()
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
            case StateType.Blocking: return target.IsBlocking;
            case StateType.Rolling: return target.IsRolling;
            case StateType.Moving: return target.IsMoving;
            case StateType.Jumping: return target.IsJumping;
            default:
                break;
        }

        return false;
    }
}
