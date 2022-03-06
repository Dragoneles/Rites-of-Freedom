/******************************************************************************
 * 
 * File: CustomEventArgs.cs
 * Author: Joseph Crump
 * Date: 2/19/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  EventArgs object for a custom event.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// EventArgs object for a custom event.
/// </summary>
public class CustomEventArgs : EventArgs
{
    public static new CustomEventArgs Empty => new CustomEventArgs();
}

public class CustomEventArgs<T0> : CustomEventArgs
{
    public readonly T0 Arg0;

    public CustomEventArgs(T0 arg0)
    {
        Arg0 = arg0;
    }
}

public class CustomEventArgs<T0, T1> : CustomEventArgs<T0>
{
    public readonly T1 Arg1;

    public CustomEventArgs(T0 arg0, T1 arg1) : base(arg0)
    {
        Arg1 = arg1;
    }
}