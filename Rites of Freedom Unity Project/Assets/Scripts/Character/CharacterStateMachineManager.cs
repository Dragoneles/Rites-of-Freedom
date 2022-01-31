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
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

/// <summary>
/// Behavior responsible for dispatching events to the
/// Gladiator's State Machines.
/// </summary>
public class CharacterStateMachineManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How long the pending inputs last before expiring.")]
    private float pendingInputDuration = 0.3f;

    public bool ControlsLocked { get; private set; } = false;

    private StateMachineCallbackDictionary callbackDictionary = 
        new StateMachineCallbackDictionary();

    /// <summary>
    /// Input operation suspended until controls are unlocked.
    /// </summary>
    private Action pendingInput { get; set; }

    /// <summary>
    /// Coroutine that handles the expiration of a pending input.
    /// </summary>
    private Coroutine pendingInputCoroutine { get; set; }

    /// <summary>
    /// This loop runs when the player starts a move action and stops when 
    /// the move action is stopped.
    /// </summary>
    private Coroutine movementCoroutine { get; set; }
    private bool movementStopped { get; set; } = false;

    /// <summary>
    /// Used to indicate which direction to move in the state machine.
    /// </summary>
    private MovementEventArgs movementArgs { get; set; }

    /// <summary>
    /// Set the control lock to true. Stop current movement actions.
    /// </summary>
    public void LockControls()
    {
        ControlsLocked = true;
    }

    public void UnlockControls()
    {
        ControlsLocked = false;
    }

    public void Move(MovementEventArgs e)
    {
        movementArgs = e;

        movementCoroutine = movementCoroutine ?? StartCoroutine(MovementCoroutine());
    }

    public void Stop()
    {
        movementStopped = true;
    }

    public void Jump()
    {
        if (ControlsLocked)
        {
            SetPendingInput(Jump);
            return;
        }

        Trigger(StateEvents.Jump);
    }

    public void Attack()
    {
        if (ControlsLocked)
        {
            SetPendingInput(Attack);
            return;
        }

        Trigger(StateEvents.Attack);

        callbackDictionary.AddCallback(
            StateType.Attack, 
            () =>
            {
                if (pendingInput != null)
                    return;

                Trigger(StateEvents.AttackStop);
            });
    }

    public void Block()
    {
        if (ControlsLocked)
        {
            SetPendingInput(Block);
            return;
        }

        Trigger(StateEvents.Block);
    }

    /// <summary>
    /// Method invoked from visual scripting to signal that a state
    /// is finished. Runs state callbacks / transitions.
    /// </summary>
    public void NotifyStateFinished(StateType state)
    {
        InvokeCallbacks(state);
        RunPendingInput();
    }

    public void RunPendingInput()
    {
        if (pendingInput == null)
            return;

        pendingInput.Invoke();

        ClearPendingInput();
    }

    /// <summary>
    /// Run a state's exit callbacks.
    /// </summary>
    private void InvokeCallbacks(StateType state)
    {
        callbackDictionary.InvokeCallbacks(state);
    }

    private void Trigger(string eventName, params object[] args)
    {
        CustomEvent.Trigger(gameObject, eventName, args);
    }

    private void ClearPendingInput()
    {
        SetPendingInput(null);
    }

    private void SetPendingInput(Action input)
    {
        pendingInput = input;
        ResetPendingInputCoroutine();
    }

    private void ResetPendingInputCoroutine()
    {
        if (pendingInputCoroutine != null)
            StopCoroutine(pendingInputCoroutine);

        if (pendingInput == null)
            return;

        pendingInputCoroutine = StartCoroutine(PendingInputCoroutine());
    }

    private IEnumerator MovementCoroutine()
    {
        while (!movementStopped)
        {
            yield return new WaitForEndOfFrame();

            if (ControlsLocked)
            {
                Trigger(StateEvents.MoveStop);
                continue;
            }

            Trigger(StateEvents.MoveStart, movementArgs);
        }

        movementArgs = null;
        movementCoroutine = null;
        movementStopped = false;
        Trigger(StateEvents.MoveStop);
    }

    private IEnumerator PendingInputCoroutine()
    {
        yield return new WaitForSeconds(pendingInputDuration);

        ClearPendingInput();

        pendingInputCoroutine = null;
    }
}

public static class StateEvents
{
    public const string MoveStart = "OnMoveStart";
    public const string MoveStop = "OnMoveStop";
    public const string Jump = "OnJump";
    public const string Attack = "OnAttack";
    public const string AttackStop = "OnAttackFinished";
    public const string Block = "OnBlock";
}
