/******************************************************************************
 * 
 * File: ContextMenuElement.cs
 * Author: Joseph Crump
 * Date: 2/18/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object containing all information needed to add an action to a context 
 *  menu.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace AI.BehaviorTree.Editor
{
    /// <summary>
    /// Object containing all information needed to add an action to a context 
    /// menu.
    /// </summary>
    public class ContextMenuElement
    {
        /// <summary>
        /// The display name of the element.
        /// </summary>
        public string Name = string.Empty;

        /// <summary>
        /// The action invoked when the element is selected.
        /// </summary>
        public Action<DropdownMenuAction> Action;

        /// <summary>
        /// Optional function for getting the status of the dropdown element.
        /// </summary>
        public Func<DropdownMenuAction, DropdownMenuAction.Status> StatusCallback;

        public ContextMenuElement(string name, Action<DropdownMenuAction> action)
        {
            Name = name;
            Action = action;
            StatusCallback = (a) => DropdownMenuAction.Status.Normal;
        }

        public ContextMenuElement(
            string name, 
            Action<DropdownMenuAction> action, 
            Func<DropdownMenuAction, DropdownMenuAction.Status> statusCallback)
        {
            Name = name;
            Action = action;
            StatusCallback = statusCallback;
        }
    }
}
