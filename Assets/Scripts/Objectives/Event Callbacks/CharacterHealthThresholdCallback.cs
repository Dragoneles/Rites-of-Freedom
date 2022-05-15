/******************************************************************************
 * 
 * File: CharacterHealthThresholdCallback.cs
 * Author: Joseph Crump
 * Date: 4/09/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Callback for when a character's health reaches a certain threshold.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Events;
using AI.BehaviorTree;

/// <summary>
/// Callback for when a character's health reaches a certain threshold.
/// </summary>
public class CharacterHealthThresholdCallback : EventCallback<VitalChangedEventArgs>
{
    [SerializeField] 
    private Character target;

    [SerializeField] 
    [Range(0f, 1f)]
    [Tooltip("The health remaining threshold that will trigger the event.")]
    private float threshold;

    [SerializeField]
    [Tooltip("How to compare the target's health to the threshold.")]
    private ComparisonType comparisonType = ComparisonType.LessThanOrEqual;

    protected override UnityEvent<VitalChangedEventArgs> Event => target.HealthChanged;

    protected override bool ValidateCallback(VitalChangedEventArgs args)
    {
        float healthPercent = args.CurrentValue / args.Vital.MaxValue;

        var comparison = new ComparablePair<float>(healthPercent, threshold);

        return comparison.Evaluate(comparisonType);
    }
}
