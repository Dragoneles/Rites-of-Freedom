/******************************************************************************
 * 
 * File: VirtualInputHandler.cs
 * Author: Joseph Crump
 * Date: 3/22/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Virtual controller, subclassed by player or AI to log context-independent
 *  input actions.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Virtual controller, subclassed by player or AI to log context-independent
/// input actions.
/// </summary>
public abstract class VirtualInputHandler : MonoBehaviour
{
    public VirtualAxisInput MovementAxis = new VirtualAxisInput();
    public VirtualInput Attack = new VirtualInput();
    public VirtualInput Block = new VirtualInput();
    public VirtualInput Jump = new VirtualInput();
    public VirtualInput Roll = new VirtualInput();
    public VirtualInput Interact = new VirtualInput();
    public VirtualInput Help = new VirtualInput();
    public VirtualInput Pause = new VirtualInput();
}

/// <summary>
/// Internal input class that contains state for an input.
/// </summary>
public class VirtualInput
{
    /// <summary>
    /// Event raised whenever the input is pressed.
    /// </summary>
    public event Action Pressed;

    /// <summary>
    /// Event raised whenever the input is released.
    /// </summary>
    public event Action Released;

    /// <summary>
    /// Returns true the frame that the input is pressed.
    /// </summary>
    public bool WasPressed { get; private set; }

    /// <summary>
    /// Returns true if the input is currently held down.
    /// </summary>
    public bool IsDown { get; private set; }

    /// <summary>
    /// Returns true the frame the input is released.
    /// </summary>
    public bool WasReleased { get; private set; }

    internal IEnumerator SetDown()
    {
        Pressed?.Invoke();

        WasPressed = true;

        IsDown = true;

        yield return new WaitForEndOfFrame();

        WasPressed = false;
    }

    internal IEnumerator SetUp()
    {
        Released?.Invoke();

        WasReleased = true;

        IsDown = false;

        yield return new WaitForEndOfFrame();

        WasReleased = false;
    }

    internal IEnumerator SimulatePress(float holdTime)
    {
        yield return SetDown();
        yield return new WaitForSeconds(holdTime);
        yield return SetUp();
    }
}

public class VirtualAxisInput
{
    /// <summary>
    /// The value of the input axis, ranging from -1 to 1.
    /// </summary>
    public float Value { get; private set; }

    internal void SetAxisValue(float value)
    {
        Value = Mathf.Clamp(value, -1f, 1f);
    }
}