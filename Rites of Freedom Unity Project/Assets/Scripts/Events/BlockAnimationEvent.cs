/******************************************************************************
 * 
 * File: BlockAnimationEvent.cs
 * Author: Joseph Crump
 * Date: 2/19/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Script that invokes an event when a Block animation starts.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Script that invokes an event when a Block animation starts.
/// </summary>
public class BlockAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<EventArgs> blocked = new UnityEvent<EventArgs>();

    public void Block()
    {
        blocked.Invoke(EventArgs.Empty);
    }
}
