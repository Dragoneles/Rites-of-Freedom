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

/// <summary>
/// Behavior attached to an animator that can invoke an event for jumps.
/// </summary>
public class JumpAnimationEvent : MonoBehaviour
{
    /// <summary>
    /// Event invoked when the character jumps.
    /// </summary>
    public SmartUnityEvent<EventArgs> Jumped = new SmartUnityEvent<EventArgs>();

    /// <summary>
    /// Invoke the jump UnityEvent.
    /// </summary>
    public void Jump()
    {
        Jumped?.Invoke(EventArgs.Empty);
    }
}
