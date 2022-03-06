/******************************************************************************
 * 
 * File: InputEventDispatcher.cs
 * Author: Joseph Crump
 * Date: 3/04/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event dispatcher for input events.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event dispatcher for input events.
/// </summary>
public class InputEventDispatcher : MonoBehaviour
{
    private CustomEvent<InputType, InputEventType> inputEvent = 
        new CustomEvent<InputType, InputEventType>();


}
