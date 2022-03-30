/******************************************************************************
 * 
 * File: BehaviorTree.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object driving the AI through nodes and conditionals.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Object driving the AI through nodes and conditionals.
    /// </summary>
    [System.Serializable]
    public class BehaviorTree : ICloneable<BehaviorTree>
    {
        public RootNode RootNode;
        public Blackboard Blackboard = new();

        public BehaviorTreeMachine machine;
        public GameObject GameObject { get => machine.gameObject; }

        [SerializeField]
        [HideInInspector]
        protected List<Node> nodes = new();

        public BehaviorTree() { }

        public BehaviorTree(BehaviorTree other)
        {
            RootNode = other.RootNode.Clone() as RootNode;
            RootNode.SetTree(this);
        }

        /// <summary>
        /// Create a cloned instance of this behavior tree and all of 
        /// its nodes.
        /// </summary>
        public BehaviorTree Clone()
        {
            return new BehaviorTree(this);
        }

        public IEnumerator Run()
        {
            yield return RootNode.Process();
        }

        public void AddNode(Node node)
        {
            nodes.Add(node);
            node.Tree = this;
        }

        public void RemoveNode(Node node)
        {
            nodes.Remove(node);
        }

        public List<Node> GetNodes() => nodes;
    }
}
