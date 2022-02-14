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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

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
        public T Get<T>(string name)
        {
            if (!initialized)
                Initialize();

            if (!dictionary.ContainsKey(name))
            {
                return default(T);
            }

            return (T)dictionary[name];
        }

        /// <summary>
        /// Write a property to the blackboard.
        /// </summary>
        public void Set<T>(string name, T value)
        {
            if (!initialized)
                Initialize();

            if (!dictionary.ContainsKey(name))
            {
                AddPropertyToDictionary(name, value);
                return;
            }

            dictionary[name] = value;
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
