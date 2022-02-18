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

        private bool initialized { get; set; } = false;
        private bool cancelled { get; set; } = false;

        public virtual Node Clone()
        {
            Node node = Instantiate(this);
            return node;
        }

        public virtual void SetTree(BehaviorTree tree)
        {
            Tree = tree;
        }

        public void SetInactive()
        {
            State = NodeState.Inactive;
        }

        /// <summary>
        /// Cancel the node's process and reset it.
        /// </summary>
        public void CancelProcess()
        {
            cancelled = true;
        }

        public IEnumerator Process()
        {
            Initialize();

            State = NodeState.Running;

            Start();

            while (State == NodeState.Running && !cancelled)
            {
                if (CheckNodeSucceeded())
                {
                    State = NodeState.Success;
                    break;
                }
                if (CheckNodeFailed())
                {
                    State = NodeState.Failure;
                    break;
                }

                yield return Run();
            }

            ResetNode();
        }

        private void Initialize()
        {
            if (initialized)
                return;

            OnInitialize();

            initialized = true;
        }

        private void ResetNode()
        {
            cancelled = false;

            OnReset();
        }

        /// <summary>
        /// Run is invoked each tick that the node is processed.
        /// </summary>
        protected virtual IEnumerator Run() { yield return null; }

        /// <summary>
        /// Conditions that indicate that the node failed.
        /// </summary>
        protected abstract bool CheckNodeFailed();

        /// <summary>
        /// Conditions that indicate that the node succeeded.
        /// </summary>
        protected abstract bool CheckNodeSucceeded();

        /// <summary>
        /// Start is invoked whenever the node beings processing.
        /// </summary>
        protected virtual void Start() { }

        /// <summary>
        /// Initialize is called only when the node is processed for 
        /// the first time.
        /// </summary>
        protected virtual void OnInitialize() { }

        /// <summary>
        /// Reset is invoked whenever the node finishes processing.
        /// </summary>
        protected virtual void OnReset() { }
    }
}
