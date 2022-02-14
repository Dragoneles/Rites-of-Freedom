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
using System.Collections.Generic;
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

        protected override bool NodeFailed()
        {
            return false;
        }

        protected override bool NodeSucceeded()
        {
            return false;
        }

        protected override void ResetNode()
        {
            Child.SetInactive();
            SetInactive();
        }

        protected override IEnumerator Run()
        {
            yield return Child.Process();
        }

        public override Node Clone(BehaviorTree tree)
        {
            RootNode node = base.Clone(tree) as RootNode;
            node.Child = Child.Clone(tree);

            return node;
        }
    }
}
