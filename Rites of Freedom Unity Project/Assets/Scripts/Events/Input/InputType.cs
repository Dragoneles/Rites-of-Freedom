/******************************************************************************
 * 
 * File: InputType.cs
 * Author: Joseph Crump
 * Date: 3/04/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Enumerator for different input events.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumerator for different input events.
/// </summary>
public enum InputType
{
    None,
    Move,
    Jump,
    Attack,
    Block,
    Roll,
    ShowControls
}
