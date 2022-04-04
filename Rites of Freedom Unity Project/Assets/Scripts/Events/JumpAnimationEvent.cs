/******************************************************************************
 * 
 * File: JumpAnimationEvent.cs
 * Author: Joseph Crump
 * Date: 3/28/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior attached to an animator that can invoke an event for jumps.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Behavior attached to an animator that can invoke an event for jumps.
/// </summary>
public class JumpAnimationEvent : MonoBehaviour
{
    /// <summary>
    /// Event invoked when the character jumps.
    /// </summary>
    public UnityEvent<EventArgs> Jumped = new();

    /// <summary>
    /// Invoke the jump UnityEvent.
    /// </summary>
    public void Jump()
    {
        Jumped?.Invoke(EventArgs.Empty);
    }
}
