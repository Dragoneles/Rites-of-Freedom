/******************************************************************************
 * 
 * File: BehaviorTreeView.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension of experimental GraphView for editing behavior trees.
 *  
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AI.BehaviorTree.Editor
{
    /// <summary>
    /// Extension of experimental GraphView for editing behavior trees.
    /// </summary>
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, UxmlTraits> { }

        public Action<NodeView> NodeSelected;

        private BehaviorTreeAsset tree;

        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/AI/BehaviorTree/Editor/BehaviorTreeEditor.uss");
            styleSheets.Add(styleSheet);

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        public void PopulateView(BehaviorTreeAsset tree)
        {
            this.tree = tree;

            graphViewChanged -= OnGraphViewChanged;

            DeleteElements(graphElements);

            graphViewChanged += OnGraphViewChanged;

            if (tree.RootNode == null)
            {
                tree.RootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
            }

            CreateNodes(tree);
            CreateEdges(tree);
        }

        private void OnUndoRedo()
        {
            PopulateView(tree);
            AssetDatabase.SaveAssets();
        }

        private void CreateEdges(BehaviorTreeAsset tree)
        {
            foreach (Node node in tree.Nodes)
            {
                NodeView parentView = GetNodeView(node);

                foreach (Node child in tree.GetChildrenOf(node))
                {
                    NodeView childView = GetNodeView(child);

                    CreateEdge(parentView, childView);
                }
            }
        }

        private void CreateNodes(BehaviorTreeAsset tree)
        {
            foreach (Node node in tree.Nodes)
            {
                CreateNodeView(node);
            }
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports
                .ToList()
                .Where((endPort) =>
                    endPort.direction != startPort.direction &&
                    endPort.node != startPort.node)
                .ToList();
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            var types = TypeCache.GetTypesDerivedFrom<Node>().ToList();

            types.RemoveAll((o) => !o.CanBeConstructed());
            types.Remove(typeof(RootNode));

            foreach (Type type in types)
            {
                Vector2 mousePosition = evt.localMousePosition;

                evt.menu.AppendAction(
                    $"[{type.BaseType.Name}] {type.Name}",
                    (a) => CreateNode(type, mousePosition));
            }
        }

        private void CreateNode(Type type, Vector2 position)
        {
            Node node = tree.CreateNode(type, position);
            CreateNodeView(node);
        }

        private void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.Selected = NodeSelected;

            AddElement(nodeView);
        }

        private void CreateEdge(NodeView parent, NodeView child)
        {
            Edge edge = new Edge();
            edge.output = parent.Output;
            edge.input = child.Input;

            AddElement(edge);
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                RemoveElements(graphViewChange);
            }

            if (graphViewChange.edgesToCreate != null)
            {
                CreateEdges(graphViewChange);
            }

            return graphViewChange;
        }

        private void RemoveElements(GraphViewChange graphViewChange)
        {
            foreach (var element in graphViewChange.elementsToRemove)
            {
                Edge edge = element as Edge;

                if (edge != null)
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;

                    tree.RemoveChild(parentView.node, childView.node);

                    continue;
                }

                NodeView nodeView = element as NodeView;

                if (nodeView != null)
                {
                    tree.DeleteNode(nodeView.node);

                    continue;
                }
            }
        }

        private void CreateEdges(GraphViewChange graphViewChange)
        {
            foreach (var edge in graphViewChange.edgesToCreate)
            {
                CreateEdge(edge);
            }
        }

        private void CreateEdge(Edge edge)
        {
            NodeView parentView = edge.output.node as NodeView;
            NodeView childView = edge.input.node as NodeView;

            tree.AddChild(parentView.node, childView.node);
        }

        private NodeView GetNodeView(Node node)
        {
            return GetNodeByGuid(node.Guid) as NodeView;
        }
    }
}