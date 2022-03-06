/******************************************************************************
 * 
 * File: Variable.cs
 * Author: Joseph Crump
 * Date: 3/04/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object reference that can be stored in the trigger editor.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crump.TriggerSystem
{
    /// <summary>
    /// Object reference that can be stored in the trigger editor.
    /// </summary>
    public class Variable
    {
        /// <summary>
        /// The name of the variable.
        /// </summary>
        public string Name = "NewVariable";

        /// <summary>
        /// The type of the variable.
        /// </summary>
        public Type Type;

        /// <summary>
        /// The stored value.
        /// </summary>
        private object value;
    }
}
