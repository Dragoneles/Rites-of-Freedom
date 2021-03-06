/******************************************************************************
 * 
 * File: ComparisonNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright ? 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Node that compares two values.
 *  
 ******************************************************************************/
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Node that compares two values.
    /// </summary>
    public class ComparisonNode : LeafNode
    {
        [SerializeField]
        [SerializeReference]
        [SerializePolymorphic]
        private ComparablePair comparablePair;

        [SerializeField]
        private ComparisonType comparisonType = ComparisonType.Equal;

        protected override bool CheckNodeSucceeded()
        {
            return comparablePair.Evaluate(comparisonType);
        }

        protected override bool CheckNodeFailed()
        {
            return !comparablePair.Evaluate(comparisonType);
        }
    }
}
