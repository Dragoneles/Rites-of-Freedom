/******************************************************************************
 * 
 * File: NGramInvoker.cs
 * Author: Joseph Crump
 * Date: 2/18/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that invokes NGram actions.
 *  
 ******************************************************************************/
using System;
using UnityEngine;

/// <summary>
/// Behavior that invokes NGram actions.
/// </summary>
public class NGramInvoker : MonoBehaviour
{
    public event Action<NGramActionInvokedEventArgs> Invoked;

    public void Invoke(NGramAction action)
    {
        Invoked?.Invoke(new NGramActionInvokedEventArgs(this, action));
    }
}
