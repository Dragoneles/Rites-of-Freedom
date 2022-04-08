/******************************************************************************
 * 
 * File: GlobalEvent.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  ScriptableObject that is used to facilitate events in the inspector.
 *  
 ******************************************************************************/
using System;
using UnityEngine;

/// <summary>
/// ScriptableObject that is used to facilitate events in the inspector.
/// </summary>
[CreateAssetMenu(fileName = "NewEvent", menuName = "Event/New Global Event")]
public class GlobalEvent : ScriptableObject, IEventObject
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
