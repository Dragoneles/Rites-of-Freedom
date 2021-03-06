/******************************************************************************
 * 
 * File: FootstepEvent.cs
 * Author: Joseph Crump
 * Date: 2/05/22
 * 
 * Copyright ? 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior attached to an animator that can invoke an event for footsteps.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Behavior attached to an animator that can invoke an event for footsteps.
/// </summary>
public class FootstepEvent : MonoBehaviour
{
    /// <summary>
    /// Event invoked when the character takes a footstep.
    /// </summary>
    public UnityEvent<EventArgs> Stepped;

    /// <summary>
    /// Invoke the footstep event.
    /// </summary>
    public void Step()
    {
        Stepped?.Invoke(EventArgs.Empty);
    }
}
