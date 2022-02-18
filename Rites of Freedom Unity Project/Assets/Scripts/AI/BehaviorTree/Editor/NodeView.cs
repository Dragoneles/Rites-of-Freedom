/******************************************************************************
 * 
 * File: BehaviorTreeNodeView.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension of Node in the experimental UI builder API for use in the 
 *  behavior tree window.
 *  
 ******************************************************************************/
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using GraphNode = UnityEditor.Experimental.GraphView.Node;

namespace AI.BehaviorTree.Editor
{
    /// <summary>
    /// Extension of Node in the experimental UI builder API for use in the 
    /// behavior tree window.
    /// </summary>
    public class NodeView : GraphNode
    {
        public Action<NodeView> Selected;
        public Action<NodeView> Unselected;

        public Node Node;

        public Port Input;
        public Port Output;

        private GraphView graph { get; set; }

        public NodeView(Node node, GraphView graph) : base("Assets/Scripts/AI/BehaviorTree/Editor/NodeView.uxml")
        {
            this.graph = graph;

            this.Node = node;
            title = node.name.Replace("Node", string.Empty);
            viewDataKey = node.Guid;

            style.left = node.Position.x;
            style.top = node.Position.y;

            SetPosition(new Rect(node.Position, new Vector2(GetWidth(), GetHeight())));

            CreateInputPorts();
            CreateOutputPorts();

            SetupClasses();
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);

            Node.Position.x = newPos.xMin;
            Node.Position.y = newPos.yMin;

            EditorUtility.SetDirty(Node);
        }

        public void UpdatePosition(Vector2 position)
        {
            Vector2 size = new Vector2(style.width.value.value, GetHeight());

            SetPosition(new Rect(position, size));

            float yOffset = GetHeight() + NodeViewUtility.verticalGap;

            if (Node is CompositeNode composite)
            {
                int childCount = composite.ChildCount;

                float width = GetWidth();

                List<Node> children = composite.GetChildren();

                float previousNodeWidth = 0f;
                float sumPreviousNodeWidths = 0f;
                for (int i = 0; i < children.Count; i++)
                {
                    NodeView childNodeView = graph.GetNodeByGuid(children[i].Guid) as NodeView;
                    float childNodeWidth = childNodeView.GetWidth();

                    float xOffset = -width / 2;
                    xOffset += (NodeViewUtility.horizontalGap * i);
                    xOffset += sumPreviousNodeWidths;
                    xOffset += childNodeWidth / 2;

                    previousNodeWidth = childNodeWidth;
                    sumPreviousNodeWidths += Mathf.Max(previousNodeWidth, NodeViewUtility.width);

                    Vector2 childPosition = position + new Vector2(xOffset, yOffset);
                    childNodeView.UpdatePosition(childPosition);
                }
            }
            else if (Node is DecoratorNode decorator)
            {
                if (decorator.Child == null)
                    return;

                NodeView childNodeView = graph.GetNodeByGuid(decorator.Child.Guid) as NodeView;
                Vector2 childPosition = position + new Vector2(0f, yOffset);
                childNodeView.UpdatePosition(childPosition);
            }
            else if (Node is RootNode root)
            {
                if (root.Child == null)
                    return;

                NodeView childNodeView = graph.GetNodeByGuid(root.Child.Guid) as NodeView;
                Vector2 childPosition = position + new Vector2(0f, yOffset);
                childNodeView.UpdatePosition(childPosition);
            }
        }

        public float GetWidth()
        {
            float width = 0f;

            if (Node is CompositeNode composite)
            {
                int childCount = composite.ChildCount;

                foreach (Node childNode in composite.GetChildren())
                {
                    if (childNode == null)
                        continue;

                    NodeView childNodeView = graph.GetNodeByGuid(childNode.Guid) as NodeView;

                    if (childNodeView != null)
                        width += childNodeView.GetWidth();
                }

                width += ((childCount - 1) * NodeViewUtility.horizontalGap);

                if (childCount > 0)
                    return width;
            }
            else if (Node is DecoratorNode decorator)
            {
                if (decorator.Child != null)
                {
                    NodeView childNodeView = graph.GetNodeByGuid(decorator.Child.Guid) as NodeView;

                    if (childNodeView != null)
                        return childNodeView.GetWidth();
                }
            }
            else if (Node is RootNode root)
            {
                if (root.Child != null)
                {
                    NodeView childNodeView = graph.GetNodeByGuid(root.Child.Guid) as NodeView;

                    if (childNodeView != null)
                        return childNodeView.GetWidth();
                }
            }

            width = NodeViewUtility.width;

            return width;
        }

        public float GetHeight()
        {
            return NodeViewUtility.height;
        }

        private void SetupClasses()
        {
            if (Node is LeafNode)
            {
                AddToClassList("leaf");
            }
            else if (Node is CompositeNode)
            {
                AddToClassList("composite");
            }
            else if (Node is DecoratorNode)
            {
                AddToClassList("decorator");
            }
            else if (Node is RootNode)
            {
                AddToClassList("root");
            }
        }

        private void CreateInputPorts()
        {
            if (Node is RootNode)
                return;

            Input = InstantiatePort(
                    Orientation.Vertical,
                    Direction.Input,
                    Port.Capacity.Single,
                    typeof(bool));

            if (Input != null)
            {
                Input.portName = string.Empty;
                Input.style.flexDirection = FlexDirection.Column;
                inputContainer.Add(Input);
            }
        }

        private void CreateOutputPorts()
        {
            if (Node is LeafNode)
            {
                // No output node
            }
            else if (Node is CompositeNode)
            {
                Output = InstantiatePort(
                    Orientation.Vertical,
                    Direction.Output,
                    Port.Capacity.Multi,
                    typeof(bool));
            }
            else if (Node is DecoratorNode)
            {
                Output = InstantiatePort(
                    Orientation.Vertical,
                    Direction.Output,
                    Port.Capacity.Single,
                    typeof(bool));
            }
            else if (Node is RootNode)
            {
                Output = InstantiatePort(
                    Orientation.Vertical,
                    Direction.Output,
                    Port.Capacity.Single,
                    typeof(bool));
            }

            if (Output != null)
            {
                Output.portName = string.Empty;
                Output.style.flexDirection = FlexDirection.ColumnReverse;
                outputContainer.Add(Output);
            }
        }

        public override void OnSelected()
        {
            Selected?.Invoke(this);
        }

        public override void OnUnselected()
        {
            Unselected?.Invoke(this);
        }
    }
}