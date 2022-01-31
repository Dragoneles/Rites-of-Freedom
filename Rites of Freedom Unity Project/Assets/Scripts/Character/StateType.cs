/******************************************************************************
 * 
 * File: StateType.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Enumerator for keying different character states.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumerator for keying different character states.
/// </summary>
public enum StateType
{
    Idle,
    Move,
    Attack,
    Block,
    Jump,
    Fall,
    Death
}
