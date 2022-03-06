/******************************************************************************
 * 
 * File: Line.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  A line in a conversation.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A line in a conversation.
/// </summary>
[System.Serializable]
public class Line
{
    public string Text = string.Empty;
    public float Duration = 5f;

    public static Line NoLine()
    {
        Line line = new Line();

        line.Text = string.Empty;
        line.Duration = 0f;

        return line;
    }
}
