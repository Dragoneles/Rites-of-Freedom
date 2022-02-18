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

        public InspectorView() { }

        public void Update(Object targetObject)
        {
            Clear();

            Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(targetObject);

            IMGUIContainer container = new IMGUIContainer(
                () =>
                {
                    if (!editor.target)
                        return;

                    editor.OnInspectorGUI();
                });

            Add(container);
        }

        public void Update(Node[] targetObjects)
        {
            Clear();

            Object.DestroyImmediate(editor);
            editor = UnityEditor.Editor.CreateEditor(targetObjects);

            if (editor == null)
                return;

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