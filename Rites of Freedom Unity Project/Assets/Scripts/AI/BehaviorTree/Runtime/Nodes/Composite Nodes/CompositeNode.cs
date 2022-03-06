/******************************************************************************
 * 
 * File: CompositeNode.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Type of behavior tree node that can have more than one child.
 *  
 ******************************************************************************/
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Type of behavior tree node that can have more than one child.
    /// </summary>
    public abstract class CompositeNode : Node
    {
        public int ChildCount { get => children.Count; }

        [SerializeField]
        [HideInInspector]
        protected List<Node> children = new List<Node>();

        /// <summary>
        /// Add a child node.
        /// </summary>
        public void AddChild(Node node)
        {
            children.Add(node);
        }

        /// <summary>
        /// Remove a child node.
        /// </summary>
        public void RemoveChild(Node node)
        {
            if (children.Contains(node))
                children.Remove(node);
        }

        public List<Node> GetChildren()
        {
            return children;
        }

        public override Node Clone()
        {
            CompositeNode node = base.Clone() as CompositeNode;
            node.children = children.ConvertAll(c => c.Clone());

            return node;
        }

        public override void SetTree(BehaviorTree tree)
        {
            base.SetTree(tree);

            foreach (Node child in children)
            {
                child.SetTree(tree);
            }
        }

        /// <summary>
        /// Swap a child node's position in the child list with the child
        /// at the index before it.
        /// </summary>
        public void MoveChildLeft(Node child)
        {
            if (ChildCount == 0)
                return;

            if (!children.Contains(child))
                return;

            if (children[0] == child)
                return;

            Node previousChild = children[children.IndexOf(child) - 1];
            SwapChildren(child, previousChild);
        }

        /// <summary>
        /// Swap a child node's position in the child list with the child
        /// at the index after it.
        /// </summary>
        public void MoveChildRight(Node child)
        {
            if (ChildCount == 0)
                return;

            if (!children.Contains(child))
                return;

            if (children[ChildCount - 1] == child)
                return;

            Node previousChild = children[children.IndexOf(child) + 1];
            SwapChildren(child, previousChild);
        }

        private void SwapChildren(Node childA, Node childB)
        {
            int childAIndex = children.IndexOf(childA);
            int childBIndex = children.IndexOf(childB);

            children[childBIndex] = childA;
            children[childAIndex] = childB;
        }

        protected override void OnReset()
        {
            foreach (Node child in children)
            {
                child.SetInactive();
            }
        }
    }
}
