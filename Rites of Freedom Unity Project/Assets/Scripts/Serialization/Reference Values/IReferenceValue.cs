/******************************************************************************
 * 
 * File: IReferenceValue.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Interface describing an item that can be cast as a specific value.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface describing an item that can be cast as a specific value.
/// </summary>
public interface IReferenceValue<T>
{
    T GetValue();
}
