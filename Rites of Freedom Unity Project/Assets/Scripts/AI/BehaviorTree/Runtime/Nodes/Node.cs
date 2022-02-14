/******************************************************************************
 * 
 * File: Node.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Base node class of the behavior tree.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Base node class of the behavior tree.
    /// </summary>
    public abstract class Node : ScriptableObject
    {
        [HideInInspector] public string Guid;
        [HideInInspector] public Vector2 Position;

        public NodeState State { get; private set; } = NodeState.Inactive;
        public BehaviorTree Tree { get; set; }
        protected BehaviorTreeMachine machine { get => Tree.machine; }
        protected GameObject gameObject { get => Tree.gameObject; }
        protected Transform transform { get => gameObject.transform; }
        protected Blackboard blackboard { get => Tree.Blackboard; }

        public virtual Node Clone(BehaviorTree tree)
        {
            Node node = Instantiate(this);
            node.Tree = tree;
            return node;
        }

        public void SetInactive()
        {
            State = NodeState.Inactive;
        }

        public IEnumerator Process()
        {
            State = NodeState.Running;

            Start();

            while (State == NodeState.Running)
            {
                if (NodeSucceeded())
                {
                    State = NodeState.Success;
                    break;
                }
                if (NodeFailed())
                {
                    State = NodeState.Failure;
                    break;
                }

                yield return Run();
            }

            ResetNode();
        }

        protected virtual void Start() { }
        protected virtual IEnumerator Run() { yield return null; }
        protected abstract bool NodeFailed();
        protected abstract bool NodeSucceeded();
        protected virtual void ResetNode() { }
    }
}
