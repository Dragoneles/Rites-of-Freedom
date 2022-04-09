/******************************************************************************
 * 
 * File: MonoBehaviourCallbackTrigger.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behaviour that invokes a UnityEvent when a MonoBehaviour callback is 
 *  called.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Behaviour that invokes a UnityEvent when a MonoBehaviour callback is 
/// called.
/// </summary>
public abstract class MonoBehaviourCallbackTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent onCallbackTriggered = new();

    protected virtual void OnTrigger()
    {
        onCallbackTriggered?.Invoke();
    }
}
