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
[RequireComponent(typeof(AIInputHandler), typeof(ITargetProvider))]
public class EnemyBehaviorTree : BehaviorTreeMachine
{
    /// <summary>
    /// The character that this character is targetting.
    /// </summary>
    public static BlackboardProperty<Character> Target = new(nameof(Target));

    /// <summary>
    /// The character that the behavior tree is attached to.
    /// </summary>
    public static BlackboardProperty<Character> Self = new(nameof(Self));

    /// <summary>
    /// The virtual input device the AI uses to perform actions.
    /// </summary>
    public static BlackboardProperty<AIInputHandler> Input = new(nameof(Input));

    /// <summary>
    /// The NGram prediction system used by the AI.
    /// </summary>
    public static BlackboardProperty<NGramPredictor> Predictor = new(nameof(Predictor));

    [SerializeField]
    private Character self;

    [SerializeField]
    private AIInputHandler input;

    [SerializeField]
    private NGramPredictor predictor;

    private ITargetProvider _targetProvider;
    private ITargetProvider targetProvider
    {
        get
        {
            if (_targetProvider == null)
                _targetProvider = GetComponent<ITargetProvider>();
            return _targetProvider;
        }
    } // lazy initialize

    private Blackboard Blackboard => Tree.Blackboard;

    private void OnValidate()
    {
        input = GetComponent<AIInputHandler>();
        self = this.GetComponentInParent<Character>();
    }

    protected override void OnTreeInitialized()
    {
        Blackboard.Set(Target, GetTarget);
        Blackboard.Set(Self, self);
        Blackboard.Set(Input, input);
        Blackboard.Set(Predictor, predictor);
    }

    /// <summary>
    /// Get the character that is using this behavior tree.
    /// </summary>
    public Character GetSelf()
    {
        return Blackboard.Get(Self);
    }

    private Character GetTarget()
    {
        return targetProvider.GetTarget();
    }
}
