/******************************************************************************
 * 
 * File: StartTrigger.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  UnityEvent that invokes on Start.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// UnityEvent that invokes on Start.
/// </summary>
public class StartTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent trigger = new UnityEvent();

    private void Start()
    {
        trigger.Invoke();
    }
}
