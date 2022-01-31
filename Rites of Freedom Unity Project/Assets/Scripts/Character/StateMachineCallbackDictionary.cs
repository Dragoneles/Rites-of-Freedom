/******************************************************************************
 * 
 * File: StateMachineCallbackDictionary.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  First-class dictionary used to add callbacks to States on exit.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// First-class dictionary used to add callbacks to States on exit.
/// </summary>
public class StateMachineCallbackDictionary
{
    private Dictionary<StateType, CallbackCollection> callbackDictionary =
        new Dictionary<StateType, CallbackCollection>();

    public void AddCallback(StateType state, Action callback)
    {
        GetCallbacks(state).AddCallback(callback);
    }

    public void InvokeCallbacks(StateType state)
    {
        GetCallbacks(state).Invoke();
    }

    private CallbackCollection GetCallbacks(StateType state)
    {
        if (!callbackDictionary.ContainsKey(state))
            AddState(state); // Lazy-initialized dictionary

        CallbackCollection callbacks;
        callbackDictionary.TryGetValue(state, out callbacks);

        return callbacks;
    }

    private void AddState(StateType state)
    {
        callbackDictionary.Add(state, new CallbackCollection());
    }
}
