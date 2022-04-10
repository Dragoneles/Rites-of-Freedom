/******************************************************************************
 * 
 * File: EventCallback.cs
 * Author: Joseph Crump
 * Date: 4/04/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Game event callback used to update objectives.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Game event callback used to update objectives.
/// </summary>
public abstract class EventCallback : MonoBehaviour 
{
    /// <summary>
    /// Raised whenever this callback is invoked.
    /// </summary>
    public event Action Invoked;

    [SerializeField]
    private UnityEvent onCallbackTriggered = new();

    protected virtual void OnInvoked()
    {
        Invoked?.Invoke();
        onCallbackTriggered?.Invoke();
    }
}

/// <summary>
/// Game event callback used to update objectives.
/// </summary>
public abstract class EventCallback<T> : EventCallback
{
    /// <summary>
    /// Event that the callback is registered to.
    /// </summary>
    protected abstract UnityEvent<T> Event { get; }

    private void Start()
    {
        Event.AddListener(OnEventRaised);
    }

    private void OnEventRaised(T args)
    {
        if (!ValidateCallback(args))
            return;

        OnInvoked();
    }

    protected override sealed void OnInvoked()
    {
        base.OnInvoked();
    }

    protected abstract bool ValidateCallback(T args);
}
