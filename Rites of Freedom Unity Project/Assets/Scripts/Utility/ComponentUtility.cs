/******************************************************************************
 * 
 * File: ComponentUtility.cs
 * Author: Joseph Crump
 * Date: 2/27/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension methods for the the Component class.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extension methods for the the Component class.
/// </summary>
public static class ComponentUtility
{
    public static Coroutine StartCoroutine(this Component component, IEnumerator routine)
    {
        var coroutineRunner = component.GetComponent<CoroutineRunner>();

        if (coroutineRunner == null)
        {
            coroutineRunner = component.gameObject.AddComponent<CoroutineRunner>();
        }

        return coroutineRunner.StartCoroutine(routine);
    }
}
