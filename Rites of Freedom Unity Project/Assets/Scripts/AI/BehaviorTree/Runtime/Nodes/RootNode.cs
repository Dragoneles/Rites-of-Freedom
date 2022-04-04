/******************************************************************************
 * 
 * File: RootNode.cs
 * Author: Joseph Crump
 * Date: 2/12/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  The node at the root of a behavior tree.
 *  
 ******************************************************************************/
using System.Collections;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// The node at the root of a behavior tree.
    /// </summary>
    public class RootNode : Node
    {
        [HideInInspector]
        public Node Child;

        public override Node Clone()
        {
            RootNode node = base.Clone() as RootNode;

            if (node.Child != null)
                node.Child = Child.Clone();

            return node;
        }

        public override void SetTree(BehaviorTree tree)
        {
            base.SetTree(tree);

            if (Child != null)
                Child.SetTree(tree);
        }

        protected override void OnReset()
        {
            Child.SetInactive();
            SetInactive();
        }

        protected override IEnumerator Run()
        {
            yield return Child.Process();
        }

        protected override bool CheckNodeFailed()
        {
            return Child.State == NodeState.Failure;
        }

        protected override bool CheckNodeSucceeded()
        {
            return Child.State == NodeState.Success;
        }
    }
}
