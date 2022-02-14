/******************************************************************************
 * 
 * File: SequenceNode.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Subclass of Node that runs children until one succeeds or all fail.
 *  
 ******************************************************************************/
using System.Collections;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Subclass of Node that runs children until one succeeds or all fail.
    /// </summary>
    public class SelectorNode : CompositeNode
    {
        private int index = 0;
        private Node currentChild => children[index];

        protected override bool NodeFailed()
        {
            foreach (Node child in children)
            {
                if (child.State != NodeState.Failure)
                    return false;
            }

            return true;
        }

        protected override bool NodeSucceeded()
        {
            foreach (Node child in children)
            {
                if (child.State == NodeState.Success)
                    return true;
            }

            return false;
        }

        protected override IEnumerator Run()
        {
            yield return currentChild.Process();

            if (currentChild.State == NodeState.Success)
                yield break;

            if (currentChild.State == NodeState.Failure)
                IncrementIndex();
        }

        private void IncrementIndex()
        {
            index++;

            if (index >= children.Count)
                index = 0;
        }

        protected override void ResetNode()
        {
            base.ResetNode();

            index = 0;
        }
    }
}
