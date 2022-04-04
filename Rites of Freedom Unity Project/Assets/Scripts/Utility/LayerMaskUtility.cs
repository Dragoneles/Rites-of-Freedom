/******************************************************************************
 * 
 * File: LayermaskUtility.cs
 * Author: Joseph Crump
 * Date: 2/15/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Static class containing extension methods for the LayerMask struct.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Static class containing extension methods for the <see cref="LayerMask"/>
/// struct.
/// </summary>
public static class LayerMaskUtility
{
    /// <summary>
    /// Check to see if a layermask contains a given layer.
    /// </summary>
    public static bool ContainsLayer(this LayerMask layermask, int layer)
    {
        return layermask == (layermask | (1 << layer));
    }
}
