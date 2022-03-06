/******************************************************************************
 * 
 * File: CustomEventHandler.cs
 * Author: Joseph Crump
 * Date: 2/19/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object responsible for raising events from a CustomEvent object.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object responsible for raising events from a CustomEvent object.
/// </summary>
public class CustomEventHandler<TArgs> where TArgs : CustomEventArgs
{
    private Action<TArgs> eventHandler;

    /// <summary>
    /// Invoke the custom event.
    /// </summary>
    /// <param name="args">
    /// The arguments that are dispatched with the event.
    /// </param>
    public void Invoke(TArgs args)
    {
        eventHandler?.Invoke(args);
    }

    /// <summary>
    /// Register a listener to the event handler.
    /// </summary>
    public void AddListener(Action<TArgs> listener)
    {
        eventHandler += listener;
    }

    /// <summary>
    /// Unregister a listener from the event handler.
    /// </summary>
    public void RemoveListener(Action<TArgs> listener)
    {
        eventHandler += listener;
    }
}
