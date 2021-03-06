/******************************************************************************
 * 
 * File: AttackAnimationEvent.cs
 * Author: Joseph Crump
 * Date: 2/19/22
 * 
 * Copyright ? 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Script that invokes an event when an Attack animation starts.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script that invokes an event when an Attack animation starts.
/// </summary>
public class AttackAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<EventArgs> attacked = new UnityEvent<EventArgs>();

    public void Attack()
    {
        attacked.Invoke(EventArgs.Empty);
    }
}
