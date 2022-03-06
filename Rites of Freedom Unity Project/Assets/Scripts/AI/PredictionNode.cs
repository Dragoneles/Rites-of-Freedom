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

    private NGramPredictor predictor { get; set; }

    protected override void OnInitialize()
    {
        predictor = blackboard.Get<NGramPredictor>(EnemyBehaviorTree.Predictor);
    }

    protected override bool CheckNodeFailed()
    {
        return !CheckNodeSucceeded();
    }

    protected override bool CheckNodeSucceeded()
    {
        if (action == null)
            return false;

        if (predictor.PredictedAction == null)
            return false;

        return action == predictor.PredictedAction;
    }
}
