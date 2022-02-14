/******************************************************************************
 * 
 * File: ReferenceValueWrapper.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension of IValueProvider that wraps an instance of the value it provides.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extension of IValueProvider that wraps an instance of the value it provides.
/// </summary>
public class ReferenceValueWrapper<T> : IReferenceValue<T>
{
    private readonly T value;

    public ReferenceValueWrapper(T value)
    {
        this.value = value;
    }

    public T GetValue()
    {
        return value;
    }

    public static implicit operator T(ReferenceValueWrapper<T> valueProvider)
    {
        return valueProvider.GetValue();
    }
}
