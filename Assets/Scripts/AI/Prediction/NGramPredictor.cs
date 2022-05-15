/******************************************************************************
 * 
 * File: NgramPredictor.cs
 * Author: Joseph Crump
 * Date: 2/17/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that monitors a character's actions, analyzes their patterns, and 
 *  predicts what will happen next.
 *  
 ******************************************************************************/
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior that monitors a character's actions, analyzes their patterns, and 
/// predicts what will happen next.
/// </summary>
public class NGramPredictor : MonoBehaviour
{
    /// <summary>
    /// The action that is predicted to be next.
    /// </summary>
    public NGramAction PredictedAction { get; private set; }

    [SerializeField]
    private TargetFinder targetFinder;

    private INGramInvoker monitoredInvoker
    {
        get
        {
            return targetFinder.GetTarget();
        }
    }

    [SerializeField]
    private List<NGramAction> observedNGramActions = new();

    [SerializeField]
    [Tooltip("Max remembered history of invoked actions.")]
    private int memorySize = 20;

    [SerializeField]
    [Min(0)]
    [Tooltip("Max number of consecutive actions that are considered in the prediction.")]
    private int sequenceLength = 3;

    private readonly List<NGramAction> actionHistory = new();

    private void Start()
    {
        RegisterNGramInvokeEvents();
    }

    private void OnDestroy()
    {
        DeregisterNGramInvokeEvents();
    }

    private void LogActionInvocation(NGramAction action)
    {
        if (actionHistory.Count > memorySize)
            actionHistory.RemoveAt(0);

        actionHistory.Add(action);

        UpdatePrediction();
    }

    private void UpdatePrediction()
    {
        List<NGramAction> mostRecentSequence = GetRecentActionSequence();

        PredictedAction = GetMostLikelyActionBasedOnSequence(mostRecentSequence);
    }

    private List<NGramAction> GetRecentActionSequence()
    {
        int sequenceEndIndex = actionHistory.Count - 1;
        int sequenceStartIndex = sequenceEndIndex - sequenceLength;

        if (sequenceStartIndex < 0)
            return new List<NGramAction>();

        List<NGramAction> sequence = actionHistory.GetRange(sequenceStartIndex, sequenceLength);

        return sequence;
    }

    private NGramAction GetMostLikelyActionBasedOnSequence(List<NGramAction> sequence)
    {
        if (actionHistory.Count == 0)
            return observedNGramActions.GetRandom();

        List<NGramAction> possiblePredictions = new();

        for (int i = 0; i < actionHistory.Count; i++)
        {
            if (i + sequenceLength >= actionHistory.Count)
                continue;

            if (actionHistory.IndexMatchesSequence(i, sequence))
            {
                possiblePredictions.Add(actionHistory[i + sequenceLength]);
            }
        }

        if (possiblePredictions.Count == 0)
            return observedNGramActions.GetRandom();

        List<NGramAction> modes = possiblePredictions.Mode();

        return modes.GetRandom();
    }

    private void RegisterNGramInvokeEvents()
    {
        if (monitoredInvoker == null)
            return;

        monitoredInvoker.Invoked += OnNGramActionInvoked;
    }

    private void DeregisterNGramInvokeEvents()
    {
        if (monitoredInvoker == null)
            return;

        monitoredInvoker.Invoked -= OnNGramActionInvoked;
    }

    protected virtual void OnNGramActionInvoked(NGramActionInvokedEventArgs e)
    {
        NGramAction action = e.Action;

        if (action == null)
            return;

        LogActionInvocation(action);
    }
}
