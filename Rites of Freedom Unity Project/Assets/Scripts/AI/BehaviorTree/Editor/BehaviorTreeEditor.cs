/******************************************************************************
 * 
 * File: BehaviorTreeEditor.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Custom editor window for setting up a behavior tree.
 *  
 ******************************************************************************/
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

namespace AI.BehaviorTree.Editor
{
    /// <summary>
    /// Custom editor window for setting up a behavior tree.
    /// </summary>
    public class BehaviorTreeEditor : EditorWindow
    {
        private BehaviorTreeView treeView;
        private InspectorView inspector;
        private IMGUIContainer blackboard;

        private SerializedObject treeObject;
        private SerializedProperty blackboardProperty;

        [SerializeField]
        private BehaviorTreeAsset tree;

        [MenuItem("BehaviorTree/Editor ...")]
        public static void OpenWindow()
        {
            BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
            wnd.titleContent = new GUIContent("BehaviorTreeEditor");
        }

        [OnOpenAsset]
        public static bool OnOpenAsset(int instanceId, int line)
        {
            if (Selection.activeObject is BehaviorTreeAsset)
            {
                OpenWindow();
                return true;
            }

            return false;
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/AI/BehaviorTree/Editor/BehaviorTreeEditor.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/AI/BehaviorTree/Editor/BehaviorTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            treeView = root.Query<BehaviorTreeView>();
            inspector = root.Query<InspectorView>();
            blackboard = root.Query<IMGUIContainer>();

            treeView.SelectionChanged += OnSelectionChanged;

            // Force the selection refresh on create
            OnSelectionChange();

            blackboard.onGUIHandler =
                () =>
                {
                    if (treeObject == null)
                        return;

                    treeObject.Update();
                    EditorGUILayout.PropertyField(blackboardProperty);
                    treeObject.ApplyModifiedProperties();
                };
        }

        private void OnSelectionChange()
        {
            var treeAsset = Selection.activeObject as BehaviorTreeAsset;

            if (treeAsset == null)
            {
                if (Selection.activeObject is GameObject gameObject)
                {
                    var treeMachine = gameObject.GetComponent<BehaviorTreeMachine>();

                    if (treeMachine != null)
                        treeAsset = treeMachine.BehaviorTree;
                }
            }

            if (treeAsset == null || !AssetDatabase.CanOpenAssetInEditor(treeAsset.GetInstanceID()))
                return;

            treeAsset.Tree ??= new BehaviorTree();

            tree = treeAsset;

            treeObject = new SerializedObject(tree);
            blackboardProperty = treeObject.FindProperty("tree").FindPropertyRelative("Blackboard");

            tree.Changed = OnAssetChanged;

            treeView.PopulateView(treeAsset);
        }

        private void OnSelectionChanged(Node[] selectedObjects)
        {
            if (selectedObjects.Length == 0)
                return;

            inspector.Update(selectedObjects);
        }

        protected virtual void OnAssetChanged(ScriptableObject asset)
        {
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
        }
    }
}