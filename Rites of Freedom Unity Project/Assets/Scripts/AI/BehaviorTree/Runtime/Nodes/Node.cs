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
using System.Collections;
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
        protected BehaviorTreeMachine Machine { get => Tree.machine; }
        protected GameObject GameObject { get => Tree.GameObject; }
        protected Transform Transform { get => GameObject.transform; }
        protected Blackboard Blackboard { get => Tree.Blackboard; }

        private bool initialized = false;
        private bool canceled = false;

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
            canceled = true;
        }

        public IEnumerator Process()
        {
            Initialize();

            State = NodeState.Running;

            Start();

            while (State == NodeState.Running && !canceled)
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
            canceled = false;

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
