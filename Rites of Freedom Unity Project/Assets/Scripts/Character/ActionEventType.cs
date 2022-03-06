/******************************************************************************
 * 
 * File: ActionEventType.cs
 * Author: Joseph Crump
 * Date: 2/19/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Values for different states that can be invoked by action events.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Values for different states that can be invoked by action events.
/// </summary>
public enum ActionEventType
{
    Started,
    Stopped,
    Performed,
    Cancelled,
    Interrupted
}
