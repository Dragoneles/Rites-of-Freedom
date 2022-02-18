/******************************************************************************
 * 
 * File: BlockNode.cs
 * Author: Joseph Crump
 * Date: 2/15/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that instructs the AI to block.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Leaf node that instructs the AI to block.
/// </summary>
public class BlockNode : LeafNode
{
    [SerializeField]
    private float blockDuration = 3f;

    private float blockTimeElapsed { get; set; } = 0f;

    private CharacterStateMachineManager stateMachine { get; set; }
    private Character self { get; set; }

    protected override void Start()
    {
        stateMachine = blackboard.Get<CharacterStateMachineManager>(EnemyBehaviorTree.StateMachine);
        self = blackboard.Get<Character>(EnemyBehaviorTree.Self);

        stateMachine.StartBlocking();
    }

    protected override void OnReset()
    {
        blockTimeElapsed = 0f;
    }

    protected override IEnumerator Run()
    {
        yield return new WaitForEndOfFrame();
        blockTimeElapsed += Time.deltaTime;
    }

    protected override bool CheckNodeFailed()
    {
        return self.IsDead;
    }

    protected override bool CheckNodeSucceeded()
    {
        bool finishedBlocking = blockTimeElapsed >= blockDuration;

        if (finishedBlocking)
            stateMachine.StopBlocking();

        return finishedBlocking;
    }
}
