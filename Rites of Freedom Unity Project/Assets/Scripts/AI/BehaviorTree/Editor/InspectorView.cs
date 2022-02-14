/******************************************************************************
 * 
 * File: InspectorView.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension of experimental InspectorView for editing behavior trees.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.UIElements;

namespace AI.BehaviorTree.Editor
{
    /// <summary>
    /// Extension of experimental InspectorView for editing behavior trees.
    /// </summary>
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, UxmlTraits> { }

        private UnityEditor.Editor editor;

        public InspectorView()
        {

        }

        public void Update(NodeView nodeView)
        {
            Clear();

            Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(nodeView.node);

            IMGUIContainer container = new IMGUIContainer(
                () => 
                {
                    if (!editor.target)
                        return;

                    editor.OnInspectorGUI();
                });

            Add(container);
        }
    }
}