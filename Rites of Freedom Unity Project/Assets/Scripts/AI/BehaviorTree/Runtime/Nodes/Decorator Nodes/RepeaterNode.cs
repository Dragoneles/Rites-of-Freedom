/******************************************************************************
 * 
 * File: RepeaterNode.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Decorator node that repeats its child a set number of times.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Decorator node that repeats its child a set number of times.
    /// </summary>
    public class RepeaterNode : DecoratorNode
    {
        [Min(-1)]
        [SerializeField]
        [Tooltip("Number of times to repeat child node. -1 repeats infinitely.")]
        protected int repeats = 3;

        [SerializeField]
        private float tickRate = 15;

        private int repeatsFinished { get; set; } = 0;

        protected override bool CheckNodeFailed() => false;

        protected override bool CheckNodeSucceeded()
        {
            return (repeatsFinished == repeats);
        }

        protected override void OnReset()
        {
            base.OnReset();

            repeatsFinished = 0;
        }

        protected override IEnumerator Run()
        {
            yield return Child.Process();

            if (Child.State == NodeState.Failure || Child.State == NodeState.Success)
            {
                if (repeats != -1)
                    repeatsFinished++;
            }

            if (repeatsFinished < repeats)
                yield return new WaitForSeconds(1f / tickRate);
        }
    }
}
