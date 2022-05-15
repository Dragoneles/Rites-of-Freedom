/******************************************************************************
 * 
 * File: Conversation.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Sequence of dialogue that can be elicited by a character.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Sequence of dialogue that can be elicited by a character.
/// </summary>
[CreateAssetMenu(fileName = "NewConversation", menuName = "Dialogue/New Conversation")]
public class Conversation : ScriptableObject
{
    public List<Line> Lines = new List<Line>();

    public int LineCount => Lines.Count;
}
