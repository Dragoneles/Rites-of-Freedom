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

        public Node node;

        public Port Input;
        public Port Output;

        public NodeView(Node node) : base("Assets/Scripts/AI/BehaviorTree/Editor/NodeView.uxml")
        {
            this.node = node;
            title = node.name.Replace("Node", string.Empty);
            viewDataKey = node.Guid;

            style.left = node.Position.x;
            style.top = node.Position.y;

            float height = GetPosition().size.y;
            float width = NodeViewUtility.NodeWidth;
            SetPosition(new Rect(node.Position, new Vector2(width, height)));

            CreateInputPorts();
            CreateOutputPorts();

            SetupClasses();
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);

            Undo.RecordObject(node, "Behavior Tree (Set Position)");

            node.Position.x = newPos.xMin;
            node.Position.y = newPos.yMin;

            EditorUtility.SetDirty(node);
        }

        private void SetupClasses()
        {
            if (node is LeafNode)
            {
                AddToClassList("leaf");
            }
            else if (node is CompositeNode)
            {
                AddToClassList("composite");
            }
            else if (node is DecoratorNode)
            {
                AddToClassList("decorator");
            }
            else if (node is RootNode)
            {
                AddToClassList("root");
            }
        }

        private void CreateInputPorts()
        {
            if (node is RootNode)
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
            if (node is LeafNode)
            {
                // No output node
            }
            else if (node is CompositeNode)
            {
                Output = InstantiatePort(
                    Orientation.Vertical,
                    Direction.Output,
                    Port.Capacity.Multi,
                    typeof(bool));
            }
            else if (node is DecoratorNode)
            {
                Output = InstantiatePort(
                    Orientation.Vertical,
                    Direction.Output,
                    Port.Capacity.Single,
                    typeof(bool));
            }
            else if (node is RootNode)
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
    }
}