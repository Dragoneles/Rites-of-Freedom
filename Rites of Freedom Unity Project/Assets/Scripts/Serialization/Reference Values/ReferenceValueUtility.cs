/******************************************************************************
 * 
 * File: ReferenceValueUtility.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Static class containing utility methods for IValueProvider.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class containing utility methods for IValueProvider.
/// </summary>
public static class ReferenceValueUtility
{
    /// <summary>
    /// Wraps an object in a value provider wrapper.
    /// </summary>
    public static IReferenceValue<T> ToReferenceValue<T>(this T obj)
    {
        return new ReferenceValueWrapper<T>(obj);
    }
}
