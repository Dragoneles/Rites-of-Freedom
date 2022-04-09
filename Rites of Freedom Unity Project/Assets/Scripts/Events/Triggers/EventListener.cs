/******************************************************************************
 * 
 * File: EventListener.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that listens for a ScriptableEvent invocation.
 *  
 ******************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Behavior that listens for a ScriptableEvent invocation.
/// </summary>
public class EventListener : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Global events that can trigger this behavior.")]
    private List<GlobalEvent> globalTriggerEvents = new();

    [SerializeField]
    [Tooltip("Local events that can trigger this behavior.")]
    private List<LocalEvent> localTriggerEvents = new();

    [SerializeField]
    [Tooltip("Actions invoked when an event is triggered.")]
    private UnityEvent onEventTriggered = new();

    private void Awake()
    {
        ValidateEvents();
    }

    private void Start()
    {
        StartListening();
    }

    private void OnEnable()
    {
        StartListening();
    }

    private void OnDisable()
    {
        StopListening();
    }

    private void OnDestroy()
    {
        StopListening();
    }

    /// <summary>
    /// Subscribe to the scriptable event.
    /// </summary>
    public void StartListening()
    {
        globalTriggerEvents.ForEach(o => o.Invoked += onEventTriggered.Invoke);
        localTriggerEvents.ForEach(o => o.Invoked += onEventTriggered.Invoke);
    }

    /// <summary>
    /// Unsubscribe from the scriptable event.
    /// </summary>
    public void StopListening()
    {
        globalTriggerEvents.ForEach(o => o.Invoked -= onEventTriggered.Invoke);
        localTriggerEvents.ForEach(o => o.Invoked -= onEventTriggered.Invoke);
    }

    private void ValidateEvents()
    {
        globalTriggerEvents.RemoveAll(o => o == null);
        localTriggerEvents.RemoveAll(o => o == null);
    }
}
