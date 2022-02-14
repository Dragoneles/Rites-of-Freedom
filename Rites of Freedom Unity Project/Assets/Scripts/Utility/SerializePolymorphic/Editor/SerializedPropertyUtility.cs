/******************************************************************************/
/**
 * File   - SerializedPropertyUtility.cs
 * Author - Joseph Crump
 * Date   - 10/09/2021
 * 
 * Copyright (c) 2021 DigiPen Institute of Technology. All rights Reserved.
 * 
 * Description:
 *   Extension class for setting and getting information for a polymorphic
 *   attribute.
 */
/******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace PhantomForge.SerializePolymorphic.Editor
{
    public static class SerializedPropertyUtility
    {
        private readonly struct PropertyInstanceParameter
        {
            public readonly Type Type;
            public readonly SerializedProperty Property;

            public PropertyInstanceParameter(Type type, SerializedProperty property)
            {
                Type = type;
                Property = property;
            }
        }

        /// <summary>
        /// Set the object value of the property to a new instance of the given type.
        /// </summary>
        public static void SetPropertyInstance(this SerializedProperty property, Type type)
        {
            object instance = Activator.CreateInstance(type);
            property.serializedObject.Update();
            property.managedReferenceValue = instance;
            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Set the object value of the property using the PropertyInstanceParameter 
        /// struct. This is necessary to assign the context menu delegate, since its
        /// parameters must be a single object class.
        /// </summary>
        public static void SetPropertyInstance(object instanceParameter)
        {
            PropertyInstanceParameter parameter = (PropertyInstanceParameter)instanceParameter;
            var property = parameter.Property;
            property.SetPropertyInstance(parameter.Type);
        }

        /// <summary>
        /// Set the object value of the property to null.
        /// </summary>
        public static void SetPropertyToNull(this SerializedProperty property)
        {
            property.serializedObject.Update();
            property.managedReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Populate the context menu with all derived type options in the appdomain.
        /// </summary>
        public static void PopulateContextMenu(this GenericMenu menu, SerializedProperty property)
        {
            // Add a Null option to the context menu
            GUIContent nullLabel = new GUIContent("Null");
            menu.AddItem(nullLabel, false, property.SetPropertyToNull);

            // Add derived types
            string parentTypeName = property.managedReferenceFieldTypename;
            Type parentType = GetTypeFromTypeName(parentTypeName);

            List<Type> types = GetDerivedTypes(parentType);
            if (TypeMeetsCriteria(parentType))
                types.Insert(0, parentType);

            foreach (Type type in types)
            {
                string label = $"{type.Name}  ({type.Namespace})";
                GUIContent contentLabel = new GUIContent(label);
                object propertyInstanceParameter = GetPropertyInstanceParameter(type, property);
                menu.AddItem(contentLabel, false, SetPropertyInstance, propertyInstanceParameter);
            }
        }

        private static Type GetTypeFromTypeName(string name)
        {
            var splitString = name.Split(char.Parse(" "));
            var className = splitString[1];
            var assemblyName = splitString[0];

            return Type.GetType($"{className}, {assemblyName}");
        }

        public static object GetPropertyInstanceParameter(Type type, SerializedProperty property)
        {
            return new PropertyInstanceParameter(type, property);
        }

        private static List<Type> GetDerivedTypes(Type parentType)
        {
            List<Type> derivedTypes = new List<Type>();
            foreach (Type derivedType in TypeCache.GetTypesDerivedFrom(parentType))
            {
                if (TypeMeetsCriteria(derivedType))
                    derivedTypes.Add(derivedType);
            }

            return derivedTypes;
        }

        private static bool TypeMeetsCriteria(Type type)
        {
            // Ignore UnityObjects since they can't be a serialized reference
            if (type.IsSubclassOf(typeof(UnityEngine.Object)))
                return false;

            // Validate type is not abstract or an interface
            if (type.IsAbstract || type.IsInterface)
                return false;

            // Type must have an empty constructor to be created by Activator
            if (type.IsClass && type.GetConstructor(Type.EmptyTypes) == null)
                return false;

            // Can't instance generic types
            if (type.ContainsGenericParameters)
                return false;

            return true;
        }
    }
}
