/******************************************************************************
 * 
 * File: SequenceNode.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Subclass of Node that runs a sequence of child nodes.
 *  
 ******************************************************************************/
using System.Collections;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Subclass of Node that runs a sequence of child nodes.
    /// </summary>
    public class SequenceNode : CompositeNode
    {
        private int index = 0;
        private Node currentChild => children[index];

        protected override bool CheckNodeFailed()
        {
            return currentChild.State == NodeState.Failure;
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

        protected override IEnumerator Run()
        {
            yield return currentChild.Process();

            if (currentChild.State == NodeState.Failure)
                yield break;

            if (currentChild.State == NodeState.Success)
                IncrementIndex();
        }

        private void IncrementIndex()
        {
            index++;

            if (index >= children.Count)
                index = 0;
        }

        protected override void OnReset()
        {
            base.OnReset();

            index = 0;
        }
    }
}
