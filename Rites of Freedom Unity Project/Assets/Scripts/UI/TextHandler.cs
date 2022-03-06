/******************************************************************************
 * 
 * File: TextHandler.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Utility behavior for manipulating text objects.
 *  
 ******************************************************************************/
using TMPro;
using UnityEngine;

/// <summary>
/// Utility behavior for manipulating text objects.
/// </summary>
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextHandler : GraphicHandler
{
    protected TextMeshProUGUI textMesh => graphic as TextMeshProUGUI;

    /// <summary>
    /// Set the Text Mesh's string value.
    /// </summary>
    public void SetText(string text)
    {
        textMesh.text = text;
    }
}
