/******************************************************************************
 * 
 * File: CustomEvent.cs
 * Author: Joseph Crump
 * Date: 2/19/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Special event class that can register listeners for only specific
 *  outcomes of raised events.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Special event class that can register listeners for only specific
/// outcomes of raised events.
/// </summary>
public class CustomEvent
{
    private CustomEventHandler<CustomEventArgs> primaryEventHandler;

    public void AddListener(Action<CustomEventArgs> listener)
        => primaryEventHandler.AddListener(listener);

    public void RemoveListener(Action<CustomEventArgs> listener)
        => primaryEventHandler.RemoveListener(listener);

    public void Invoke()
    {
        primaryEventHandler?.Invoke(CustomEventArgs.Empty);
    }
}

/// <summary>
/// Special event class that can register listeners for only specific
/// outcomes of raised events.
/// </summary>
public class CustomEvent<T0>
{
    private CustomEventHandler<CustomEventArgs<T0>> primaryEventHandler;

    private Dictionary<T0, CustomEventHandler<CustomEventArgs<T0>>> fixedEventHandlers
        = new Dictionary<T0, CustomEventHandler<CustomEventArgs<T0>>>();

    /// <summary>
    /// Indexer for a custom event handler. Accesses the event by its 
    /// signature.
    /// </summary>
    /// <param name="arg">
    /// The event handler's signature.
    /// </param>
    public CustomEventHandler<CustomEventArgs<T0>> this[T0 arg]
    {
        get
        {
            if (!fixedEventHandlers.ContainsKey(arg))
                fixedEventHandlers.Add(arg, new CustomEventHandler<CustomEventArgs<T0>>());

            return fixedEventHandlers[arg];
        }
    }

    public void AddListener(Action<CustomEventArgs<T0>> listener)
        => primaryEventHandler.AddListener(listener);

    public void RemoveListener(Action<CustomEventArgs<T0>> listener)
        => primaryEventHandler.RemoveListener(listener);

    public void Invoke(T0 arg0)
    {
        primaryEventHandler?.Invoke(new CustomEventArgs<T0>(arg0));
        TryGetHandler(arg0)?.Invoke(new CustomEventArgs<T0>(arg0));
    }

    private CustomEventHandler<CustomEventArgs<T0>> TryGetHandler(T0 arg0)
    {
        if (!fixedEventHandlers.ContainsKey(arg0))
            return null;

        return fixedEventHandlers[arg0];
    }
}

/// <summary>
/// Special event class that can register listeners for only specific
/// outcomes of raised events.
/// </summary>
public class CustomEvent<T0, T1>
{
    private CustomEventHandler<CustomEventArgs<T0,T1>> primaryEventHandler;

    private Dictionary<T0, CustomEventHandler<CustomEventArgs<T0, T1>>> arg0FixedEventHandlers
        = new Dictionary<T0, CustomEventHandler<CustomEventArgs<T0, T1>>>();

    private Dictionary<T1, CustomEventHandler<CustomEventArgs<T0, T1>>> arg1FixedEventHandlers
        = new Dictionary<T1, CustomEventHandler<CustomEventArgs<T0, T1>>>();

    private Dictionary<(T0, T1), CustomEventHandler<CustomEventArgs<T0, T1>>> dualFixedEventHandlers
        = new Dictionary<(T0, T1), CustomEventHandler<CustomEventArgs<T0, T1>>>();

    /// <summary>
    /// Indexer for a custom event handler. Accesses the event by its 
    /// signature.
    /// </summary>
    /// <param name="arg">
    /// The event handler's signature.
    /// </param>
    public CustomEventHandler<CustomEventArgs<T0, T1>> this[T0 arg]
    {
        get
        {
            if (!arg0FixedEventHandlers.ContainsKey(arg))
                arg0FixedEventHandlers.Add(arg, new CustomEventHandler<CustomEventArgs<T0, T1>>());

            return arg0FixedEventHandlers[arg];
        }
    }

    public CustomEventHandler<CustomEventArgs<T0, T1>> this[T1 arg]
    {
        get
        {
            if (!arg1FixedEventHandlers.ContainsKey(arg))
                arg1FixedEventHandlers.Add(arg, new CustomEventHandler<CustomEventArgs<T0, T1>>());

            return arg1FixedEventHandlers[arg];
        }
    }

    public CustomEventHandler<CustomEventArgs<T0, T1>> this[T0 a, T1 b]
    {
        get
        {
            if (!dualFixedEventHandlers.ContainsKey((a, b)))
                dualFixedEventHandlers.Add((a, b), new CustomEventHandler<CustomEventArgs<T0, T1>>());

            return dualFixedEventHandlers[(a, b)];
        }
    }

    public void AddListener(Action<CustomEventArgs<T0, T1>> listener)
        => primaryEventHandler.AddListener(listener);

    public void RemoveListener(Action<CustomEventArgs<T0, T1>> listener)
        => primaryEventHandler.RemoveListener(listener);

    public void Invoke(T0 arg0, T1 arg1)
    {
        primaryEventHandler?.Invoke(new CustomEventArgs<T0, T1>(arg0, arg1));
        TryGetHandler(arg0)?.Invoke(new CustomEventArgs<T0, T1>(arg0, arg1));
        TryGetHandler(arg1)?.Invoke(new CustomEventArgs<T0, T1>(arg0, arg1));
        TryGetHandler(arg0, arg1)?.Invoke(new CustomEventArgs<T0, T1>(arg0, arg1));
    }

    private CustomEventHandler<CustomEventArgs<T0, T1>> TryGetHandler(T0 arg0)
    {
        if (!arg0FixedEventHandlers.ContainsKey(arg0))
            return null;

        return arg0FixedEventHandlers[arg0];
    }

    private CustomEventHandler<CustomEventArgs<T0, T1>> TryGetHandler(T1 arg1)
    {
        if (!arg1FixedEventHandlers.ContainsKey(arg1))
            return null;

        return arg1FixedEventHandlers[arg1];
    }

    private CustomEventHandler<CustomEventArgs<T0, T1>> TryGetHandler(T0 arg0, T1 arg1)
    {
        if (!dualFixedEventHandlers.ContainsKey((arg0, arg1)))
            return null;

        return dualFixedEventHandlers[(arg0, arg1)];
    }
}
