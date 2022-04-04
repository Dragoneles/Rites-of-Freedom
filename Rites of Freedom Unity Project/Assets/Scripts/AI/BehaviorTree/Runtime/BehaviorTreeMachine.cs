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

        protected BehaviorTree Tree { get; set; }
        private Coroutine treeRunner;

        private void Awake()
        {
            InitializeTree();
        }

        private void Start()
        {
            Run();
        }

        public void SetTree(BehaviorTreeAsset tree)
        {
            if (treeRunner != null)
            {
                StopCoroutine(treeRunner);
                treeRunner = null;
            }    

            BehaviorTree = tree;

            InitializeTree();

            Run();
        }

        private void InitializeTree()
        {
            if (BehaviorTree == null)
                return;

            Tree = BehaviorTree.Clone();

            Tree.machine = this;

            OnTreeInitialized();
        }

        protected virtual void OnTreeInitialized() { }

        private void Run()
        {
            if (treeRunner != null)
                return;

            if (Tree == null)
                return;

            treeRunner = StartCoroutine(Tree.Run());
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
