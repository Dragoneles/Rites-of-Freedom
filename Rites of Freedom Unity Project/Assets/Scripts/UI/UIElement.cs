/******************************************************************************
 * 
 * File: UIElement.cs
 * Author: Joseph Crump
 * Date: 2/27/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Base class for all objects that belong to the UI.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for all objects that belong to the UI.
/// </summary>
public abstract class UIElement : MonoBehaviour
{
    /// <summary>
    /// All possible identifiers for this UI element.
    /// </summary>
    public List<string> IDs = new List<string>();
}
