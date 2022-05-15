/******************************************************************************/
/**
 * File   - SerializePolymorphicDrawer.cs
 * Author - Joseph Crump
 * Date   - 10/08/2021
 * 
 * Copyright (c) 2021 DigiPen Institute of Technology. All rights Reserved.
 * 
 * Description:
 *   Editor property drawer for a SerializePolymorphicAttribute.
 */
/******************************************************************************/
using UnityEngine;
using UnityEditor;

namespace PhantomForge.SerializePolymorphic.Editor
{
    [CustomPropertyDrawer(typeof(SerializePolymorphicAttribute))]
    public class SerializePolymorphicDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Label the button using the polymorphic serialized type
            string propertyLabel = property.displayName;
            string buttonLabelText = GetPropertyReferenceTypeClassName(property);
            GUIContent buttonLabel = new GUIContent(buttonLabelText);

            if (SerializePolymorphicDropDownButton(position, propertyLabel, buttonLabel, 
                                                   out Rect buttonPosition))
            {
                var menu = new GenericMenu();
                menu.PopulateContextMenu(property);
                menu.DropDown(buttonPosition);
            }

            EditorGUI.PropertyField(position, property, true);
            EditorGUI.EndProperty();
        }

        private string GetPropertyReferenceTypeClassName(SerializedProperty property)
        {
            string propertyReferenceType = property.managedReferenceFullTypename;

            if (string.IsNullOrEmpty(propertyReferenceType))
                return "Null";

            string[] splitString = propertyReferenceType.Split(char.Parse(" "));
            return splitString[1];
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        private static bool SerializePolymorphicDropDownButton(Rect position, string propertyLabel, GUIContent buttonLabel, out Rect buttonPosition)
        {
            float height = EditorGUIUtility.singleLineHeight;

            // Draw label for the property
            float labelWidth = EditorGUIUtility.labelWidth;
            Rect labelPosition = new Rect(position.x, position.y, labelWidth, height);
            EditorGUI.LabelField(labelPosition, propertyLabel);

            // Draw button
            float buttonWidth = position.width - labelWidth;
            float buttonX = position.x + labelWidth;
            buttonPosition = new Rect(buttonX, position.y, buttonWidth, height);
            return EditorGUI.DropdownButton(buttonPosition, buttonLabel, FocusType.Passive);
        }
    }
}
