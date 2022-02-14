/******************************************************************************
 * 
 * File: RandomNode.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Composite node that chooses a child to execute at random.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Composite node that chooses a child to execute at random.
    /// </summary>
    public class RandomNode : CompositeNode
    {
        private Node selectedChild { get; set; }

        protected override bool NodeFailed()
        {
            return selectedChild.State == NodeState.Failure;
        }

        protected override bool NodeSucceeded()
        {
            return selectedChild.State == NodeState.Success;
        }

        protected override void Start()
        {
            int randomIndex = Random.Range(0, children.Count);
            selectedChild = children[randomIndex];
        }

        protected override IEnumerator Run()
        {
            yield return selectedChild.Process();
        }
    }
}
