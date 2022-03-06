/******************************************************************************
 * 
 * File: DistanceNode.cs
 * Author: Joseph Crump
 * Date: 2/19/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior tree node that evaluates distance from the target.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using UnityEngine;

/// <summary>
/// Behavior tree node that evaluates distance from the target.
/// </summary>
public class DistanceNode : LeafNode
{
    [SerializeField]
    private ComparisonType compareType = ComparisonType.GreaterThan;

    [SerializeField]
    private float value = 0f;

    private Character self { get; set; }
    private Character target { get; set; }

    protected override void OnInitialize()
    {
        self = blackboard.Get<Character>(EnemyBehaviorTree.Self);
        target = blackboard.Get<Character>(EnemyBehaviorTree.Target);
    }

    protected override bool CheckNodeFailed()
    {
        return !CheckNodeSucceeded();
    }

    protected override bool CheckNodeSucceeded()
    {
        float distance = Vector2.Distance(self.Position, target.Position);
        var comparablePair = new ComparablePair<float>(distance, value);

        return comparablePair.Evaluate(compareType);
    }
}
