/******************************************************************************
 * 
 * File: NotNode.cs
 * Author: Joseph Crump
 * Date: 2/11/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Leaf node that tells the agent to wait.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Leaf node that tells the agent to wait.
    /// </summary>
    public class WaitNode : LeafNode
    {
        [SerializeField]
        protected float waitDuration = 1f;

        private bool doneWaiting { get; set; } = false;

        protected override bool CheckNodeSucceeded() => doneWaiting;
        protected override bool CheckNodeFailed() => false;

        protected override IEnumerator Run()
        {
            yield return new WaitForSeconds(waitDuration);
            doneWaiting = true;
        }

        protected override void OnReset()
        {
            doneWaiting = false;
        }
    }
}
