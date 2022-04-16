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
using UnityEngine;

/// <summary>
/// Leaf node that instructs the AI to block.
/// </summary>
public class BlockNode : LeafNode
{
    [SerializeField]
    private float blockDuration = 3f;

    private float blockTimeElapsed = 0f;

    private Character self;
    private AIInputHandler input;

    protected override void OnNodeEntered()
    {
        blockTimeElapsed = 0f;

        self = Blackboard.Get(EnemyBehaviorTree.Self);
        input = Blackboard.Get(EnemyBehaviorTree.Input);

        input.PerformBlock(blockDuration);
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

        return finishedBlocking;
    }
}
