/******************************************************************************
 * 
 * File: EnemyBehaviorTree.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior tree machine made specifically for this game's AI.
 *  
 * Remarks:
 *  Fully developing the behavior tree tool takes too much time to work for 
 *  this project, so this is sort of hacking things together.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using UnityEngine;

/// <summary>
/// Behavior tree machine made specifically for this game's AI.
/// </summary>
public class EnemyBehaviorTree : BehaviorTreeMachine
{
    public const string Self = nameof(self);
    public const string Target = nameof(target);
    public const string StateMachine = nameof(stateMachine);
    public const string Predictor = nameof(predictor);

    [SerializeField]
    private Character self;

    [SerializeField]
    private Character target;

    [SerializeField]
    private CharacterStateMachineManager stateMachine;

    [SerializeField]
    private NGramPredictor predictor;

    private Blackboard blackboard => tree.Blackboard;

    protected override void Awake()
    {
        base.Awake();

        blackboard.Set(nameof(self), self);
        blackboard.Set(nameof(target), target);
        blackboard.Set(nameof(stateMachine), stateMachine);
        blackboard.Set(nameof(predictor), predictor);
    }
}
