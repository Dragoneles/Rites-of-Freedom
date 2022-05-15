/******************************************************************************
 * 
 * File: MonobehaviorUtility.cs
 * Author: Joseph Crump
 * Date: 3/21/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension methods for MonoBehaviour.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extension methods for MonoBehaviour.
/// </summary>
public static class MonoBehaviourUtility
{
    /// <summary>
    /// Perform an action after waiting for a delay.
    /// </summary>
    /// <param name="action">
    /// The callback action to invoke.
    /// </param>
    /// <param name="delay">
    /// Length of the delay in seconds.
    /// </param>
    public static void DoAfterDelay(this MonoBehaviour behavior, Action action, float delay)
    {
        behavior.StartCoroutine(DelayInvoke(action, delay));
    }

    private static IEnumerator DelayInvoke(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);

        action.Invoke();
    }
}
