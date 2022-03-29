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
using System.Collections.Generic;
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
    private const float MinimumPressLength = 0.05f;

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

    /// <summary>
    /// Treat the input as if it were pressed. Until the frame ends, 
    /// WasPressed will evaluate to true.
    /// </summary>
    internal void SetDown(MonoBehaviour coroutineRunner)
    {
        Pressed?.Invoke();

        WasPressed = true;

        IsDown = true;

        coroutineRunner.StartCoroutine(ResetDown());
    }

    /// <summary>
    /// Treat the input as if it were released. Until the frame ends, 
    /// WasReleased will evaluate to true.
    /// </summary>
    internal void SetUp(MonoBehaviour coroutineRunner)
    {
        Released?.Invoke();

        WasReleased = true;

        IsDown = false;

        coroutineRunner.StartCoroutine(ResetUp());
    }

    /// <summary>
    /// Simulate a virtual press and release action.
    /// </summary>
    /// <param name="holdTime">
    /// The duration that the button is down.
    /// </param>
    internal void SimulatePress(MonoBehaviour coroutineRunner, float holdTime)
    {
        holdTime = Mathf.Max(holdTime, MinimumPressLength);

        coroutineRunner.StartCoroutine(SimulatePress(holdTime));
    }

    private IEnumerator ResetDown()
    {
        yield return new WaitForEndOfFrame();

        WasPressed = false;
    }

    private IEnumerator ResetUp()
    {
        yield return new WaitForEndOfFrame();

        WasReleased = false;
    }

    private IEnumerator SimulatePress(float holdTime)
    {
        yield return ResetDown();
        yield return new WaitForSeconds(holdTime);
        yield return ResetUp();
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