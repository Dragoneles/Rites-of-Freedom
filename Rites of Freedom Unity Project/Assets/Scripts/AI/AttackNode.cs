/******************************************************************************
 * 
 * File: AttackNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that tells the AI to attack.
 *  
 ******************************************************************************/
using AI.BehaviorTree;

/// <summary>
/// Leaf node that tells the AI to attack.
/// </summary>
public class AttackNode : LeafNode
{
    private CharacterStateMachineManager stateMachine { get; set; }

    protected override void OnInitialize()
    {
        stateMachine = blackboard.Get<CharacterStateMachineManager>(EnemyBehaviorTree.StateMachine);
    }

    protected override void Start()
    {
        stateMachine.Attack();
    }

    protected override bool CheckNodeFailed()
    {
        return !CheckNodeSucceeded();
    }

    protected override bool CheckNodeSucceeded()
    {
        return stateMachine.NextActionState == StateType.Attack || 
            stateMachine.CurrentActionState == StateType.Attack;
    }
}
