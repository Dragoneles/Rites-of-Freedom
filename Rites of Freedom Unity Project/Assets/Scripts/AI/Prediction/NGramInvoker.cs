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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityObject = UnityEngine.Object;

/// <summary>
/// Behavior that invokes NGram actions.
/// </summary>
public class NGramInvoker : MonoBehaviour
{
    public event EventHandler<NGramActionInvokedEventArgs> Invoked;

    public void Invoke(NGramAction action)
    {
        Invoked?.Invoke(this, new NGramActionInvokedEventArgs(this, action));
    }
}
