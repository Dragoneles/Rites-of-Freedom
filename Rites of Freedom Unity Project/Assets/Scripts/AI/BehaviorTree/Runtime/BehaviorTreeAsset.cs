/******************************************************************************
 * 
 * File: BehaviorTreeAsset.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  ScriptableObject asset wrapper for a Behavior Tree.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AI.BehaviorTree
{
    /// <summary>
    /// ScriptableObject asset wrapper for a Behavior Tree.
    /// </summary>
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "AI/BehaviorTree")]
    public class BehaviorTreeAsset : ScriptableObject
    {
        public Action<BehaviorTreeAsset> Changed;

        [SerializeField]
        private BehaviorTree tree = new BehaviorTree();
        public BehaviorTree Tree { get => tree; set => tree = value; }
        public RootNode RootNode { get => Tree.RootNode; set => Tree.RootNode = value; }
        public List<Node> Nodes => Tree.GetNodes();

        /// <summary>
        /// Create a new node of the specified type.
        /// </summary>
        public Node CreateNode(Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
#if UNITY_EDITOR
            node.Guid = GUID.Generate().ToString();
#endif

#if UNITY_EDITOR
            Undo.RecordObject(this, "Behavior Tree (Create Node)");
#endif
            Tree.AddNode(node);

#if UNITY_EDITOR
            AssetDatabase.AddObjectToAsset(node, this);
#endif
#if UNITY_EDITOR
            Undo.RegisterCreatedObjectUndo(node, "Behavior Tree (Create Node)");
#endif
            Changed?.Invoke(this);

            return node;
        }

        /// <summary>
        /// Create a new node of the specified type.
        /// </summary>
        public Node CreateNode(Type type, Node parent)
        {
            Node node = CreateNode(type);
            AddChild(parent, node);

            return node;
        }

        public void DeleteNode(Node node)
        {
            if (node == null)
                return;

#if UNITY_EDITOR
            Undo.RecordObject(this, "Behavior Tree (Delete Node)");
#endif

            Tree.RemoveNode(node);

#if UNITY_EDITOR
            Undo.DestroyObjectImmediate(node);
#endif

            Changed?.Invoke(this);
        }

        public void AddChild(Node parent, Node child)
        {
            if (parent is CompositeNode compositeNode)
            {
#if UNITY_EDITOR
                Undo.RecordObject(compositeNode, "Behavior Tree (Add Child)");
#endif
                compositeNode.AddChild(child);
#if UNITY_EDITOR
                EditorUtility.SetDirty(compositeNode);
#endif
            }
            else if (parent is DecoratorNode decoratorNode)
            {
#if UNITY_EDITOR
                Undo.RecordObject(decoratorNode, "Behavior Tree (Add Child)");
#endif
                decoratorNode.Child = child;
#if UNITY_EDITOR
                EditorUtility.SetDirty(decoratorNode);
#endif
            }
            else if (parent is RootNode rootNode)
            {
#if UNITY_EDITOR
                Undo.RecordObject(rootNode, "Behavior Tree (Add Child)");
#endif
                rootNode.Child = child;
#if UNITY_EDITOR
                EditorUtility.SetDirty(rootNode);
#endif
            }

            Changed?.Invoke(this);
        }

        public void RemoveChild(Node parent, Node child)
        {
            if (parent is CompositeNode compositeNode)
            {
#if UNITY_EDITOR
                Undo.RecordObject(compositeNode, "Behavior Tree (Remove Child)");
#endif
                compositeNode.RemoveChild(child);
#if UNITY_EDITOR
                EditorUtility.SetDirty(compositeNode);
#endif
            }
            else if (parent is DecoratorNode decoratorNode)
            {
#if UNITY_EDITOR
                Undo.RecordObject(decoratorNode, "Behavior Tree (Remove Child)");
#endif
                decoratorNode.ClearChild();
#if UNITY_EDITOR
                EditorUtility.SetDirty(decoratorNode);
#endif
            }
            else if (parent is RootNode rootNode)
            {
#if UNITY_EDITOR
                Undo.RecordObject(rootNode, "Behavior Tree (Remove Child)");
#endif
                rootNode.Child = null;
#if UNITY_EDITOR
                EditorUtility.SetDirty(rootNode);
#endif
            }

            Changed?.Invoke(this);
        }

        public List<Node> GetChildrenOf(Node parent)
        {
            var children = new List<Node>();

            if (parent is CompositeNode compositeNode)
            {
                children = compositeNode.GetChildren();
            }
            else if (parent is DecoratorNode decoratorNode)
            {
                if (decoratorNode.Child != null)
                    children.Add(decoratorNode.Child);
            }
            else if (parent is RootNode rootNode)
            {
                if (rootNode.Child != null)
                    children.Add(rootNode.Child);
            }

            return children;
        }

        public BehaviorTree Clone()
        {
            return Tree.Clone();
        }
    }
}
