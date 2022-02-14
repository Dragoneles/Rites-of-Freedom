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

        public override Node Clone(BehaviorTree tree)
        {
            CompositeNode node = base.Clone(tree) as CompositeNode;
            node.children = children.ConvertAll(c => c.Clone(tree));

            return node;
        }

        protected override void ResetNode()
        {
            foreach (Node child in children)
            {
                child.SetInactive();
            }
        }
    }
}
