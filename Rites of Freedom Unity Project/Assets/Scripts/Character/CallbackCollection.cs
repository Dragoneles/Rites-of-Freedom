/******************************************************************************
 * 
 * File: StateCallbackCollection.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Collection of callbacks.
 *  
 ******************************************************************************/
using System;
using System.Collections.Generic;

/// <summary>
/// Collection of callbacks.
/// </summary>
public class CallbackCollection
{
    private List<Action> callbacks = new List<Action>();

    public void AddCallback(Action callback)
    {
        callbacks.Add(callback);
    }

    public void Invoke()
    {
        foreach (Action callback in callbacks)
        {
            callback.Invoke();
        }

        callbacks.Clear();
    }
}
