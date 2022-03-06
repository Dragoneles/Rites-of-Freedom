/******************************************************************************
 * 
 * File: UIElementGroup.cs
 * Author: Joseph Crump
 * Date: 2/27/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  A group of UI elements.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A group of UI elements.
/// </summary>
public class UIElementGroup : List<UIElement>
{
    public void RemoveNullReferences()
    {
        RemoveAll((o) => o == null);
    }
}
