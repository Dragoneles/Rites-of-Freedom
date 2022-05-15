/******************************************************************************
 * 
 * File: IEventObject.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object that can be subscribed to that dispatches inspector-driven events.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// Object that can be subscribed to that dispatches inspector-driven events.
/// </summary>
public interface IEventObject
{
    /// <summary>
    /// C# event dispatcher, raised when the EventObject is invoked.
    /// </summary>
    public event Action Invoked;

    /// <summary>
    /// Invoke this event object.
    /// </summary>
    void Invoke();

    /// <summary>
    /// Invoke the event object after a delay.
    /// </summary>
    void DelayInvoke(float delay);
}
