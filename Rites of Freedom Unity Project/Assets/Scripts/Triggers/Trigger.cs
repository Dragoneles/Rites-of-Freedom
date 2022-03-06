/******************************************************************************
 * 
 * File: Trigger.cs
 * Author: Joseph Crump
 * Date: 3/04/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Base element of the trigger system.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crump.TriggerSystem
{
    /// <summary>
    /// Base element of the trigger system.
    /// </summary>
    public class Trigger
    {
        /// <summary>
        /// Events that can cause this trigger to run.
        /// </summary>
        public List<TriggerEvent> Events = new List<TriggerEvent>();

        /// <summary>
        /// Conditions that must be met for this trigger to run.
        /// </summary>
        public Condition TriggerCondition;

        /// <summary>
        /// Local variables used by this trigger.
        /// </summary>
        public List<Variable> LocalVariables = new List<Variable>();

        /// <summary>
        /// Actions that execute when this trigger runs.
        /// </summary>
        public List<TriggerAction> Actions = new List<TriggerAction>();
    }
}

