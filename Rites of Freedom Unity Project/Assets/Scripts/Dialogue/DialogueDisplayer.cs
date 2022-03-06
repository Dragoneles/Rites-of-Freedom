/******************************************************************************
 * 
 * File: DialogueDisplayer.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior used to display written lines of dialogue.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior used to display written lines of dialogue.
/// </summary>
public class DialogueDisplayer : MonoBehaviour
{
    [SerializeField]
    private TextHandler textHandler;

    /// <summary>
    /// Write a dialogue line to the display.
    /// </summary>
    public void Display(Line line)
    {
        textHandler.SetText(line.Text);
    }
}
