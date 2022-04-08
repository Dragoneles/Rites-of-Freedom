/******************************************************************************
 * 
 * File: EventObject.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Container implementing the behavior of an EventObject.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

/// <summary>
/// Container implementing the behavior of an EventObject.
/// </summary>
[Serializable]
public class EventObject : IEventObject
{
    public event Action Invoked;

    [SerializeField]
    private UnityEvent onEventInvoked = new();

    public void Invoke()
    {
        onEventInvoked?.Invoke();
        Invoked?.Invoke();
    }

    public void DelayInvoke(float delay)
    {
        Sequence delayInvoke = DOTween.Sequence();
        delayInvoke.SetDelay(delay);
        delayInvoke.OnStart(Invoke);

        delayInvoke.Play();
    }
}
