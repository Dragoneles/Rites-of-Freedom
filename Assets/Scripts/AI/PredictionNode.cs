/******************************************************************************
 * 
 * File: PredictionNode.cs
 * Author: Joseph Crump
 * Date: 2/19/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior tree node that evaluates an NGram prediction.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using UnityEngine;

/// <summary>
/// Behavior tree node that evaluates an NGram prediction.
/// </summary>
public class PredictionNode : LeafNode
{
    [SerializeField]
    private NGramAction action;

    private NGramPredictor predictor;

    [Range(0f, 1f)]
    [SerializeField]
    [Tooltip("Chance to treat the NGram prediction as a success.")]
    private float bypassChance = 0f;

    private float bypassRoll = 0f;

    protected override void OnInitialize()
    {
        predictor = Blackboard.Get(EnemyBehaviorTree.Predictor);
    }

    protected override bool CheckNodeFailed()
    {
        bypassRoll = Random.Range(0f, 1f);

        return !CheckNodeSucceeded();
    }

    protected override bool CheckNodeSucceeded()
    {
        if (EvaluateBypassChance())
            return true;

        if (action == null)
            return false;

        if (predictor.PredictedAction == null)
            return false;

        return action == predictor.PredictedAction;
    }

    private bool EvaluateBypassChance()
    {
        if (bypassRoll < bypassChance)
            return true;

        // default
        return false;
    }
}
