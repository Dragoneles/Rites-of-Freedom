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

        public Action<Node[]> SelectionChanged;

        private List<NodeView> selectedNodeViews = new List<NodeView>();

        private BehaviorTreeAsset tree;

        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
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

            CreateNodeViews(tree);
            CreateEdges(tree);

            RearrangeNodes();
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
            if (selectedNodeViews.Count == 0)
                return;

            AppendCreateActions(evt);

            evt.menu.AppendAction($"Delete", (a) => DeleteSelection());
        }

        private void AppendCreateActions(ContextualMenuPopulateEvent evt)
        {
            NodeView firstSelectedNodeView = selectedNodeViews[0];

            if (firstSelectedNodeView.Node is LeafNode)
                return;

            List<Type> types = GetConstructableNodeTypes();

            foreach (Type type in types)
            {
                Vector2 mousePosition = evt.localMousePosition;

                string baseName = type.BaseType.Name.Replace("Node", " Node");
                string typeName = type.Name.Replace("Node", string.Empty);

                evt.menu.AppendAction(
                    $"Add Child/{baseName}/{typeName}",
                    (a) => CreateNode(type, firstSelectedNodeView.Node));
            }
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

        private Edge CreateEdge(NodeView parent, NodeView child)
        {
            Edge edge = parent.Output.ConnectTo(child.Input);

            AddElement(edge);

            return edge;
        }

        private void CreateNodeViews(BehaviorTreeAsset tree)
        {
            bool doNotRearrange = false;

            foreach (Node node in tree.Nodes)
            {
                CreateNodeView(node, doNotRearrange);
            }

            RearrangeNodes();
        }

        private NodeView CreateNodeView(Node node, bool rearrangeAfterCreate = true)
        {
            NodeView nodeView = new NodeView(node, this);
            nodeView.Selected += OnNodeViewSelected;
            nodeView.Unselected += OnNodeViewUnselected;

            AddElement(nodeView);

            if (rearrangeAfterCreate)
                RearrangeNodes();

            return nodeView;
        }

        private void RearrangeNodes()
        {
            var rootNodeView = GetNodeByGuid(tree.RootNode.Guid) as NodeView;

            rootNodeView.UpdatePosition(Vector2.zero);
        }

        private static List<Type> GetConstructableNodeTypes()
        {
            var types = TypeCache.GetTypesDerivedFrom<Node>().ToList();

            types.RemoveAll((o) => !o.CanBeConstructed());
            types.Remove(typeof(RootNode));

            var sortedTypes = types
                .OrderBy((o => o.BaseType.Name))
                .ThenBy(o => o.Name)
                .ToList();

            return sortedTypes;
        }

        private void CreateNode(Type type, Node parent)
        {
            Node node = tree.CreateNode(type, parent);
            NodeView nodeView = CreateNodeView(node);
            CreateEdge(GetNodeByGuid(parent.Guid) as NodeView, nodeView);
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if (graphViewChange.elementsToRemove != null)
            {
                RemoveElements(graphViewChange);
            }

            if (graphViewChange.edgesToCreate != null)
            {
                HandleEdgesCreated(graphViewChange);
            }

            return graphViewChange;
        }

        private void RemoveElements(GraphViewChange graphViewChange)
        {
            foreach (var element in graphViewChange.elementsToRemove)
            {
                TryRemoveEdge(element);
                TryRemoveNode(element);
            }

            RearrangeNodes();
        }

        private void TryRemoveNode(GraphElement element)
        {
            NodeView nodeView = element as NodeView;

            if (nodeView != null)
            {
                tree.DeleteNode(nodeView.Node);
            }
        }

        private void TryRemoveEdge(GraphElement element)
        {
            Edge edge = element as Edge;

            if (edge != null)
            {
                NodeView parentView = edge.output.node as NodeView;
                NodeView childView = edge.input.node as NodeView;

                tree.RemoveChild(parentView.Node, childView.Node);
            }
        }

        private void HandleEdgesCreated(GraphViewChange graphViewChange)
        {
            foreach (var edge in graphViewChange.edgesToCreate)
            {
                HandleEdgeCreated(edge);
            }
        }

        private void HandleEdgeCreated(Edge edge)
        {
            NodeView parentView = edge.output.node as NodeView;
            NodeView childView = edge.input.node as NodeView;

            tree.AddChild(parentView.Node, childView.Node);

            Add(edge);
        }

        private NodeView GetNodeView(Node node)
        {
            return GetNodeByGuid(node.Guid) as NodeView;
        }

        private List<Node> GetNodesFromSelection()
        {
            List<Node> nodes = new List<Node>();

            foreach (NodeView nodeView in selectedNodeViews)
            {
                nodes.Add(nodeView.Node);
            }

            return nodes;
        }

        private void UpdateSelection()
        {
            List<Node> selectedNodes = GetNodesFromSelection();

            SelectionChanged?.Invoke(selectedNodes.ToArray());
        }

        protected virtual void OnNodeViewSelected(NodeView nodeView)
        {
            selectedNodeViews.Add(nodeView);

            UpdateSelection();
        }

        protected virtual void OnNodeViewUnselected(NodeView nodeView)
        {
            selectedNodeViews.Remove(nodeView);

            UpdateSelection();
        }
    }
}