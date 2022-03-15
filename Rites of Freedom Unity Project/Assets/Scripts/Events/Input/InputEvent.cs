/******************************************************************************
 * 
 * File: InputEvent.cs
 * Author: Joseph Crump
 * Date: 3/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event wrapper for inputs from a character.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event wrapper for inputs from a character.
/// </summary>
public class InputEvent
{
    /// <summary>
    /// Event invoked whenever the input is released.
    /// </summary>
    public event Action Pressed;

    /// <summary>
    /// Event invoked every frame while the input is held.
    /// </summary>
    public event Action Held;

    /// <summary>
    /// Event invoked whenever the input is released.
    /// </summary>
    public event Action Released;

    public bool IsPressed { get; private set; } = false;
    public bool IsHeld { get; private set; } = false;
    public bool IsReleased { get; private set; } = false;

    /// <summary>
    /// Set whether this input is up or down.
    /// </summary>
    public void SetDown(bool value)
    {

    }
}
