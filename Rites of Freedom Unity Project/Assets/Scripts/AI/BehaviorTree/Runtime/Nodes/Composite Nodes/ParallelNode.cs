/******************************************************************************
 * 
 * File: ParallelNode.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Composite node that runs its children simultaneously, succeeding when
 *  one succeeds and failing when any of them fail.
 *  
 ******************************************************************************/
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Subclass of Node that runs a sequence of child nodes.
    /// </summary>
    public class ParallelNode : CompositeNode
    {
        private List<Coroutine> childrenProcesses = new List<Coroutine>();

        protected override void OnNodeEntered()
        {
            foreach (Node child in children)
            {
                childrenProcesses.Add(Machine.StartCoroutine(child.Process()));
            }
        }

        protected override void OnReset()
        {
            foreach (Node child in children)
            {
                if (child.State == NodeState.Running)
                    child.CancelProcess();
            }

            foreach (Coroutine process in childrenProcesses)
            {
                if (process != null)
                    Machine.StopCoroutine(process);
            }

            childrenProcesses.Clear();

            base.OnReset();
        }

        protected override bool CheckNodeFailed()
        {
            foreach (Node child in children)
            {
                if (child.State == NodeState.Failure)
                    return true;
            }

            return false;
        }

        protected override bool CheckNodeSucceeded()
        {
            foreach (Node child in children)
            {
                if (child.State != NodeState.Success)
                    return false;
            }

            return true;
        }
    }
}
