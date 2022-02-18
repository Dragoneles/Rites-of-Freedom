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
            node.Child = Child.Clone();

            return node;
        }

        public override void SetTree(BehaviorTree tree)
        {
            base.SetTree(tree);

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
            if (Child.State == NodeState.Failure)
            {
                Debug.Log("Root node failed");
            }

            return Child.State == NodeState.Failure;
        }

        protected override bool CheckNodeSucceeded()
        {
            if (Child.State == NodeState.Success)
            {
                Debug.Log("Root node succeeded");
            }

            return Child.State == NodeState.Success;
        }
    }
}
