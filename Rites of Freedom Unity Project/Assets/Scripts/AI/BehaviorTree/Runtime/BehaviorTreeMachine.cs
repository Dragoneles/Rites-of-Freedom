/******************************************************************************
 * 
 * File: BehaviorTreeMachine.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Component that runs a BehaviorTree.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Component that runs a BehaviorTree.
    /// </summary>
    public class BehaviorTreeMachine : MonoBehaviour
    {
        public BehaviorTreeAsset BehaviorTree;

        protected BehaviorTree tree { get; set; }
        private Coroutine treeRunner { get; set; }

        protected virtual void Awake()
        {
            tree = BehaviorTree.Clone();
            tree.machine = this;
        }

        private void Start()
        {
            Run();
        }

        private void Run()
        {
            if (treeRunner != null)
                return;

            treeRunner = StartCoroutine(tree.Run());
        }

        private void OnEnable()
        {
            Run();
        }

        private void OnDisable()
        {
            if (treeRunner == null)
                return;

            StopCoroutine(treeRunner);
        }
    }
}
