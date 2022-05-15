/******************************************************************************
 * 
 * File: Blackboard.cs
 * Author: Joseph Crump
 * Date: 2/12/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Variable blackboard for a behavior tree.
 *  
 ******************************************************************************/
using System;
using System.Collections.Generic;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Variable blackboard for a behavior tree.
    /// </summary>
    [System.Serializable]
    public class Blackboard
    {
        private Dictionary<string, object> dictionary = new Dictionary<string, object>();

        private bool initialized { get; set; } = false;

        /// <summary>
        /// Get a property from the blackboard.
        /// </summary>
        public T Get<T>(BlackboardProperty<T> property)
        {
            if (!initialized)
                Initialize();

            if (!dictionary.ContainsKey(property.Name))
            {
                return property.GetDefault();
            }

            if (dictionary[property.Name] is Func<T> reference)
                return reference.Invoke();

            return (T)dictionary[property.Name];
        }

        /// <summary>
        /// Write a property to the blackboard.
        /// </summary>
        public void Set<T>(BlackboardProperty<T> property, T value)
        {
            if (!initialized)
                Initialize();

            if (!dictionary.ContainsKey(property.Name))
            {
                AddPropertyToDictionary(property.Name, value);
                return;
            }

            dictionary[property.Name] = value;
        }

        /// <summary>
        /// Set a property to return a reference to a value.
        /// </summary>
        public void Set<T>(BlackboardProperty<T> property, Func<T> reference)
        {
            if (!initialized)
                Initialize();

            if (!dictionary.ContainsKey(property.Name))
            {
                AddPropertyToDictionary(property.Name, reference);
                return;
            }

            dictionary[property.Name] = reference;
        }

        private void Initialize()
        {
            dictionary = new Dictionary<string, object>();

            initialized = true;
        }

        private void AddPropertyToDictionary<T>(string name, T value)
        {
            if (dictionary.ContainsKey(name))
            {
                throw new Exception($"Blackboard contains duplicate name '{name}'");
            }

            dictionary.Add(name, value);
        }
    }
}
