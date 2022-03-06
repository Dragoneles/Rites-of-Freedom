/******************************************************************************
 * 
 * File: Condition.cs
 * Author: Joseph Crump
 * Date: 3/04/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Conditional that can be serialized and evaluated in the trigger editor.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crump.TriggerSystem
{
    /// <summary>
    /// Conditional that can be serialized and evaluated in the trigger editor.
    /// </summary>
    public abstract class Condition
    {
        /// <summary>
        /// Evaluate this condition.
        /// </summary>
        public abstract bool Evaluate();
    }
}
