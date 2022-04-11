/******************************************************************************
 * 
 * File: CounterEventCallback.cs
 * Author: Joseph Crump
 * Date: 4/10/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Callback behavior for a counter object.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Events;
using AI.BehaviorTree;

/// <summary>
/// Callback behavior for a counter object.
/// </summary>
public class CounterEventCallback : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onThresholdReached = new();

    [SerializeField]
    [Tooltip("Value the counter must reach for the callback to fire.")]
    private int eventThreshold = 3;

    [SerializeField]
    [Tooltip("How to compare the current value to the threshold.")]
    private ComparisonType comparisonType = ComparisonType.Equal;

    private int counterValue = 0;

    /// <summary>
    /// Increase the counter by 1.
    /// </summary>
    public void IncreaseCounter()
    {
        counterValue++;

        if (ValidateConditions())
            onThresholdReached?.Invoke();
    }

    private bool ValidateConditions()
    {
        var comparison = new ComparablePair<int>(counterValue, eventThreshold);

        return comparison.Evaluate(comparisonType);
    }
}
