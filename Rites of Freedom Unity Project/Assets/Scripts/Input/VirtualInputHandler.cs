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
using UnityEngine.Events;

/// <summary>
/// Virtual controller, subclassed by player or AI to log context-independent
/// input actions.
/// </summary>
public abstract class VirtualInputHandler : MonoBehaviour
{
    public VirtualAxisInput MovementAxis = new();
    public VirtualInput Attack = new();
    public VirtualInput Block = new();
    public VirtualInput Jump = new();
    public VirtualInput Roll = new();
    public VirtualInput Interact = new();
    public VirtualInput Help = new();
    public VirtualInput Pause = new();

    /// <summary>
    /// Get a list of all virtual inputs that match a given types enum.
    /// </summary>
    /// <param name="types">
    /// Flags enum for different input types.
    /// </param>
    public List<VirtualInput> GetInputsByTypes(InputTypes types)
    {
        List<VirtualInput> inputs = new();

        if (types.HasFlag(InputTypes.Attack))
            inputs.Add(Attack);
        if (types.HasFlag(InputTypes.Block))
            inputs.Add(Block);
        if (types.HasFlag(InputTypes.Jump))
            inputs.Add(Jump);
        if (types.HasFlag(InputTypes.Roll))
            inputs.Add(Roll);
        if (types.HasFlag(InputTypes.Interact))
            inputs.Add(Interact);
        if (types.HasFlag(InputTypes.Help))
            inputs.Add(Help);
        if (types.HasFlag(InputTypes.Pause))
            inputs.Add(Pause);

        return inputs;
    }
}

/// <summary>
/// Flags enum that can be mapped to virtual inputs.
/// </summary>
[Flags]
public enum InputTypes
{
    None = 0,
    Attack = 1,
    Block = 2,
    Jump = 4,
    Roll = 8,
    Interact = 16,
    Help = 32,
    Pause = 64
}

/// <summary>
/// Internal input class that contains state for an input.
/// </summary>
[Serializable]
public class VirtualInput
{
    private const float MinimumPressLength = 0.05f;

    /// <summary>
    /// Event raised whenever the input is pressed.
    /// </summary>
    public UnityEvent Pressed;

    /// <summary>
    /// Event raised whenever the input is released.
    /// </summary>
    public UnityEvent Released;

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

        coroutineRunner.StartCoroutine(PerformVirtualPress(coroutineRunner, holdTime));
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

    private IEnumerator PerformVirtualPress(MonoBehaviour coroutineRunner, float holdTime)
    {
        SetDown(coroutineRunner);
        yield return new WaitForSeconds(holdTime);
        SetUp(coroutineRunner);
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