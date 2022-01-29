/******************************************************************************
 * 
 * File: CharacterStateMachineManager.cs
 * Author: Joseph Crump
 * Date: 1/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior responsible for dispatching events to the Gladiator's 
 *  State Machine.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using Unity.VisualScripting;

/// <summary>
/// Behavior responsible for dispatching events to the
/// Gladiator's State Machines.
/// </summary>
public class CharacterStateMachineManager : MonoBehaviour
{
    public bool ControlsLocked { get; private set; } = false;

    /// <summary>
    /// Input operation suspended until controls are unlocked.
    /// </summary>
    private Action pendingInput { get; set; }

    /// <summary>
    /// Set the control lock to true. Stop current movement actions.
    /// </summary>
    public void LockControls()
    {
        Stop();

        ControlsLocked = true;
    }

    public void UnlockControls()
    {
        pendingInput.Invoke();
        ClearPendingInput();

        ControlsLocked = false;
    }

    public void Move(MovementEventArgs e)
    {
        if (ControlsLocked)
            return;

        Trigger(StateEvents.MoveStart, e);
    }

    public void Stop()
    {
        if (ControlsLocked)
            return;

        Trigger(StateEvents.MoveStop);
    }

    public void Jump()
    {
        if (ControlsLocked)
        {
            pendingInput = Jump;
            return;
        }

        Trigger(StateEvents.Jump);
    }

    public void Attack()
    {
        if (ControlsLocked)
        {
            pendingInput = Attack;
            return;
        }

        Trigger(StateEvents.Attack);
    }

    public void Block()
    {
        if (ControlsLocked)
        {
            pendingInput = Block;
            return;
        }

        Trigger(StateEvents.Block);
    }

    private void Trigger(string eventName, params object[] args)
    {
        CustomEvent.Trigger(gameObject, eventName, args);
    }

    private void ClearPendingInput()
    {
        pendingInput = null;
    }
}

public static class StateEvents
{
    public const string MoveStart = "OnMoveStart";
    public const string MoveStop = "OnMoveStop";
    public const string Jump = "OnJump";
    public const string Attack = "OnAttack";
    public const string Block = "OnBlock";
}
