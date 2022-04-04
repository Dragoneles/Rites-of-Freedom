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
    /// <summary>
    /// The character that the behavior tree is attached to.
    /// </summary>
    public static BlackboardProperty<Character> Self = new(nameof(Self));

    /// <summary>
    /// The character that this character is targetting.
    /// </summary>
    public static BlackboardProperty<Character> Target = new(nameof(Target));

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
    private Character target;

    [SerializeField]
    private AIInputHandler input;

    [SerializeField]
    private NGramPredictor predictor;

    private Blackboard Blackboard => Tree.Blackboard;

    protected override void OnTreeInitialized()
    {
        Blackboard.Set(Self, self);
        Blackboard.Set(Target, target);
        Blackboard.Set(Input, input);
        Blackboard.Set(Predictor, predictor);
    }
}
