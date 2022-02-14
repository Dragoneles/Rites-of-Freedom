/******************************************************************************
 * 
 * File: TypeUtility.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Static extension methods for the System.Type class.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEditor;

namespace AI.BehaviorTree.Editor
{
    /// <summary>
    /// Static extension methods for the System.Type class.
    /// </summary>
    public static class TypeUtility
    {
        public static bool CanBeConstructed(this Type type)
        {
            if (type.IsAbstract) return false;
            if (type.IsInterface) return false;
            if (type.IsGenericType) return false;

            return true;
        }

        /// <summary>
        /// Get the name of the type as a human-readable name.
        /// </summary>
        public static string GetName(this Type type)
        {
            return nameof(type);
        }

        public static Func<Rect, object, object> GetEditorField<T>(this T type) where T : Type
        {
            if (type.IsAssignableFrom(typeof(UnityEngine.Object)))
                return 
                (position, obj) =>
                {
                    return EditorGUI.ObjectField(position, obj as UnityEngine.Object, type, true);
                };

            if (type == typeof(int))
                return
                (position, value) =>
                {
                    return EditorGUI.IntField(position, (int)value);
                };

            if (type == typeof(float))
                return
                (position, value) =>
                {
                    return EditorGUI.FloatField(position, (float)value);
                };
            if (type == typeof(string))
                return
                (position, value) =>
                {
                    return EditorGUI.TextField(position, (string)value);
                };

            return null;
        }
    }
}