/******************************************************************************
 * 
 * File: DecoratorNode.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright � 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Type of behavior tree node that has one child and performs some type of
 *  modifier to its return status.
 *  
 ******************************************************************************/
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Type of behavior tree node that has one child and performs some type of
    /// modifier to its return status.
    /// </summary>
    public abstract class DecoratorNode : Node
    {
        [HideInInspector]
        public Node Child;

        public void ClearChild()
        {
            Child = null;
        }

        public override Node Clone(BehaviorTree tree)
        {
            DecoratorNode node = base.Clone(tree) as DecoratorNode;
            node.Child = Child.Clone(tree);

            return node;
        }

        protected override void ResetNode()
        {
            Child.SetInactive();
        }
    }
}
