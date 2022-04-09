/******************************************************************************
 * 
 * File: InputEvent.cs
 * Author: Joseph Crump
 * Date: 4/08/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event callback for a player's input operation.
 *  
 ******************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event callback for a player's input operation.
/// </summary>
public class InputEvent : MonoBehaviour
{
    [SerializeField]
    private InputTypes inputTypes;

    [SerializeField]
    private List<VirtualInputHandler> observedInputHandlers = new();

    [SerializeField]
    private UnityEvent onInputUsed = new();

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
        List<VirtualInput> inputs = new();
        observedInputHandlers.ForEach(o => inputs.AddRange(o.GetInputsByTypes(inputTypes)));
        inputs.ForEach(o => o.Released.AddListener(onInputUsed.Invoke));
    }

    /// <summary>
    /// Unsubscribe from the scriptable event.
    /// </summary>
    public void StopListening()
    {
        List<VirtualInput> inputs = new();
        observedInputHandlers.ForEach(o => inputs.AddRange(o.GetInputsByTypes(inputTypes)));
        inputs.ForEach(o => o.Released.RemoveListener(onInputUsed.Invoke));
    }
}
