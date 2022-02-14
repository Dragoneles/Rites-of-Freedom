/******************************************************************************
 * 
 * File: EnemyDistanceNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Node that checks the distance between the AI an its opponent.
 *  
 ******************************************************************************/
using AI.BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Node that checks the distance between the AI and its opponent.
/// </summary>
public class EnemyDistanceNode : LeafNode
{
    [SerializeField]
    private ComparisonType comparisonType = ComparisonType.LessThan;

    [SerializeField]
    private float value;

    protected override bool NodeFailed()
    {
        var enemy = blackboard.Get<Character>(EnemyBehaviorTree.Target);
        var self = blackboard.Get<Character>(EnemyBehaviorTree.Self);
        float distance = Vector3.Distance(enemy.transform.position, self.transform.position);
        var comparePair = new ComparablePair<float>(distance, value);

        return !comparePair.Evaluate(comparisonType);
    }

    protected override bool NodeSucceeded()
    {
        var enemy = blackboard.Get<Character>(EnemyBehaviorTree.Target);
        var self = blackboard.Get<Character>(EnemyBehaviorTree.Self);
        float distance = Vector3.Distance(enemy.transform.position, self.transform.position);
        var comparePair = new ComparablePair<float>(distance, value);

        return comparePair.Evaluate(comparisonType);
    }
}
