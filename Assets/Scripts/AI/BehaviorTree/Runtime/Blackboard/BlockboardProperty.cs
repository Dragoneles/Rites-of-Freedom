/******************************************************************************
 * 
 * File: BlackboardProperty.cs
 * Author: Joseph Crump
 * Date: 3/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Strongly typed property identifier for properties on a blackboard.
 *  
 ******************************************************************************/
using System;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Strongly typed property identifier for properties on a blackboard.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the property value it identifies.
    /// </typeparam>
    public class BlackboardProperty<T>
    {
        /// <summary>
        /// Name of the blackboard property.
        /// </summary>
        public string Name { get; set; }

        // Method returning a default value. Could return a reference or a
        // default value, set by the property constructor.
        Func<T> _defaultValueFunc;

        public BlackboardProperty()
        {
            Name = Guid.NewGuid().ToString();
        }

        public BlackboardProperty(string name) : this(name, default(T)) { }

        public BlackboardProperty(string name, T defaultValue)
        {
            Name = name;
            _defaultValueFunc = () => defaultValue;
        }

        /// <remarks>
        /// Use this constructor if the default value is a reference type, and you
        /// do not want to share the same instance across multiple blackboards.  
        /// </remarks>
        public BlackboardProperty(string name, Func<T> createDefaultValueFunc)
        {
            Name = name;
            _defaultValueFunc = createDefaultValueFunc;
        }

        public T GetDefault()
        {
            return _defaultValueFunc();
        }
    }
}
