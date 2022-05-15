/******************************************************************************
 * 
 * File: LocalEvent.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  MonoBehavior that is used to facilitate events in the inspector.
 *  
 ******************************************************************************/
using System;
using UnityEngine;

/// <summary>
/// ScriptableObject that is used to facilitate events in the inspector.
/// </summary>
[AddComponentMenu("Events/Local Event")]
public class LocalEvent : MonoBehaviour, IEventObject
{
    [SerializeField]
    private EventObject eventObject = new();

    public event Action Invoked
    {
        add
        {
            ((IEventObject)eventObject).Invoked += value;
        }

        remove
        {
            ((IEventObject)eventObject).Invoked -= value;
        }
    }

    public void DelayInvoke(float delay)
    {
        ((IEventObject)eventObject).DelayInvoke(delay);
    }

    public void Invoke()
    {
        ((IEventObject)eventObject).Invoke();
    }
}
