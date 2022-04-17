/******************************************************************************
 * 
 * File: INGramInvoker.cs
 * Author: Joseph Crump
 * Date: 4/16/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Interface describing an object that can invoke NGrams.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// Interface describing an object that can invoke NGrams.
/// </summary>
public interface INGramInvoker
{
    /// <summary>
    /// Event raised when the NGram is invoked.
    /// </summary>
    public event Action<NGramActionInvokedEventArgs> Invoked;

    /// <summary>
    /// Invoke the given NGram object.
    /// </summary>
    void Invoke(NGramAction action);
}
