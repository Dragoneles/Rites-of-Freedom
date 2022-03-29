/******************************************************************************
 * 
 * File: UIElement.cs
 * Author: Joseph Crump
 * Date: 2/27/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Base class for all objects that belong to the UI.
 *  
 ******************************************************************************/
using System;
using UnityEngine;

/// <summary>
/// Base class for all objects that belong to the UI.
/// </summary>
public class UIElement : MonoBehaviour
{
    /// <summary>
    /// Event raised when a UI element is created.
    /// </summary>
    public static Action<UIElement> Created;

    /// <summary>
    /// Event raised when a UI element is destroyed.
    /// </summary>
    public static Action<UIElement> Destroyed;

    /// <summary>
    /// All possible identifiers for this UI element.
    /// </summary>
    public string ID = string.Empty;

    private void Start()
    {
        OnCreated();

        OnStart();
    }

    /// <summary>
    /// Virtual factory method for executing logic on Awake().
    /// </summary>
    protected virtual void OnStart() { }

    private void OnDestroy()
    {
        OnDestroyed();
    }

    /// <summary>
    /// Raise the <see cref="Created"/> event.
    /// </summary>
    protected virtual void OnCreated()
    {
        Created?.Invoke(this);
    }

    /// <summary>
    /// Raised the <see cref="Destroyed"/> event.
    /// </summary>
    protected virtual void OnDestroyed()
    {
        Destroyed?.Invoke(this);
    }
}
