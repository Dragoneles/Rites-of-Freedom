/******************************************************************************
 * 
 * File: NGramInvokedEventArgs.cs
 * Author: Joseph Crump
 * Date: 2/18/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event arguments containing information about an Ngram invocation.
 *  
 ******************************************************************************/
using System;
using UnityObject = UnityEngine.Object;

/// <summary>
/// Event arguments containing information about an Ngram invocation.
/// </summary>
public class NGramActionInvokedEventArgs : EventArgs
{
    /// <summary>
    /// The Object that invoked the event.
    /// </summary>
    public readonly UnityObject Invoker;

    /// <summary>
    /// The action that was invoked.
    /// </summary>
    public readonly NGramAction Action;

    public NGramActionInvokedEventArgs(UnityObject invoker, NGramAction action)
    {
        Invoker = invoker;
        Action = action;
    }
}
