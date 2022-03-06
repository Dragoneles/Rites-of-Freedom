/******************************************************************************
 * 
 * File: RollNode.cs
 * Author: Joseph Crump
 * Date: 2/18/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that tells the AI to roll.
 *  
 ******************************************************************************/
using AI.BehaviorTree;

/// <summary>
/// Leaf node that tells the AI to jump.
/// </summary>
public class RollNode : LeafNode
{
    private CharacterStateMachineManager stateMachine { get; set; }
    private Character self { get; set; }

    protected override void OnInitialize()
    {
        stateMachine = blackboard.Get<CharacterStateMachineManager>(EnemyBehaviorTree.StateMachine);
        self = blackboard.Get<Character>(EnemyBehaviorTree.Self);
    }

    protected override void Start()
    {
        stateMachine.Roll();
    }

    protected override bool CheckNodeFailed()
    {
        return !CheckNodeSucceeded();
    }

    protected override bool CheckNodeSucceeded()
    {
        return self.IsRolling;
    }
}
