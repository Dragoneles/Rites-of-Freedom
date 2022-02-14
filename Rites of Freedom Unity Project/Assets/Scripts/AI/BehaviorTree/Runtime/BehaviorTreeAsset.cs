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
            return CreateNode(type, Vector2.zero);
        }

        /// <summary>
        /// Create a new node of the specified type.
        /// </summary>
        public Node CreateNode(Type type, Vector2 position)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.Guid = GUID.Generate().ToString();
            node.Position = position;

            Undo.RecordObject(this, "Behavior Tree (Create Node)");
            Tree.AddNode(node);

            AssetDatabase.AddObjectToAsset(node, this);
            Undo.RegisterCreatedObjectUndo(node, "Behavior Tree (Create Node)");
            Changed?.Invoke(this);

            return node;
        }

        public void DeleteNode(Node node)
        {
            Undo.RecordObject(this, "Behavior Tree (Delete Node)");
            Tree.RemoveNode(node);

            Undo.DestroyObjectImmediate(node);

            Changed?.Invoke(this);
        }

        public void AddChild(Node parent, Node child)
        {
            if (parent is CompositeNode compositeNode)
            {
                Undo.RecordObject(compositeNode, "Behavior Tree (Add Child)");
                compositeNode.AddChild(child);
                EditorUtility.SetDirty(compositeNode);
            }
            else if (parent is DecoratorNode decoratorNode)
            {
                Undo.RecordObject(decoratorNode, "Behavior Tree (Add Child)");
                decoratorNode.Child = child;
                EditorUtility.SetDirty(decoratorNode);
            }
            else if (parent is RootNode rootNode)
            {
                Undo.RecordObject(rootNode, "Behavior Tree (Add Child)");
                rootNode.Child = child;
                EditorUtility.SetDirty(rootNode);
            }

            Changed?.Invoke(this);
        }

        public void RemoveChild(Node parent, Node child)
        {
            if (parent is CompositeNode compositeNode)
            {
                Undo.RecordObject(compositeNode, "Behavior Tree (Remove Child)");
                compositeNode.RemoveChild(child);
                EditorUtility.SetDirty(compositeNode);
            }
            else if (parent is DecoratorNode decoratorNode)
            {
                Undo.RecordObject(decoratorNode, "Behavior Tree (Remove Child)");
                decoratorNode.ClearChild();
                EditorUtility.SetDirty(decoratorNode);
            }
            else if (parent is RootNode rootNode)
            {
                Undo.RecordObject(rootNode, "Behavior Tree (Remove Child)");
                rootNode.Child = null;
                EditorUtility.SetDirty(rootNode);
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
