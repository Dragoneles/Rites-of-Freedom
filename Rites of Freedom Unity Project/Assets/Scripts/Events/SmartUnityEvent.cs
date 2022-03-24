/******************************************************************************
 * 
 * File: SmartUnityEvent.cs
 * Author: Joseph Crump
 * Date: 3/23/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Wrapper for a UnityEvent that also raises C# events and 
 *  visual scipt events.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Wrapper for a UnityEvent that also raises C# events and 
/// visual scipt events.
/// </summary>
/// <typeparam name="TArgs">
/// The event's arguments.
/// </typeparam>
[Serializable]
public class SmartUnityEvent<TArgs>
{
    private event UnityAction<TArgs> csEvent;

    [SerializeField]
    private UnityEvent<TArgs> unityEvent;

    /// <summary>
    /// Invoke this event.
    /// </summary>
    public void Invoke(TArgs args)
    {
        unityEvent?.Invoke(args);
        csEvent?.Invoke(args);
    }

    /// <summary>
    /// Add a callback for the event.
    /// </summary>
    public void AddListener(UnityAction<TArgs> listener)
    {
        csEvent += listener;
        unityEvent.AddListener(listener);
    }

    /// <summary>
    /// Remove a callback from the event.
    /// </summary>
    public void RemoveListener(UnityAction<TArgs> listener)
    {
        csEvent -= listener;
        unityEvent.RemoveListener(listener);
    }
}
