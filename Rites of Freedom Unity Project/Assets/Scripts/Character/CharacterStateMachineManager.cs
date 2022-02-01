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
[RequireComponent(typeof(Character))]
public class CharacterStateMachineManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("How long the pending inputs last before expiring.")]
    private float pendingInputDuration = 0.3f;

    public bool ControlsLocked { get; private set; } = false;

    private StateMachineCallbackDictionary callbackDictionary = 
        new StateMachineCallbackDictionary();

    private Character character { get; set; }

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
    private bool movementStopped { get; set; } = false;

    private void Awake()
    {
        character = GetComponent<Character>();
        character.GroundStateChanged += OnCharacterGroundStateChanged;
        character.YVelocityChanged += OnCharacterYVelocityChanged;
    }

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

    public void Move(float direction)
    {
        if (ControlsLocked || direction == 0f)
        {
            Trigger(StateEvents.MoveStop);
            return;
        }

        Trigger(StateEvents.MoveStart, direction);
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

    public void BlockStart()
    {
        if (ControlsLocked)
        {
            SetPendingInput(BlockStart);
            return;
        }

        Trigger(StateEvents.BlockStart);
    }

    public void BlockStop()
    {
        Trigger(StateEvents.BlockStop);
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

    private IEnumerator PendingInputCoroutine()
    {
        yield return new WaitForSeconds(pendingInputDuration);

        ClearPendingInput();

        pendingInputCoroutine = null;
    }

    protected virtual void OnCharacterGroundStateChanged(object sender, BoolEventArgs e)
    {
        if (e == false)
            return;

        Trigger(StateEvents.Land);
    }

    protected virtual void OnCharacterYVelocityChanged(object sender, FloatEventArgs e)
    {
        if (character.Feet.IsGrounded)
            return;

        if (e < 0f)
            Trigger(StateEvents.Fall);
    }

    protected virtual void OnCharacterBlocked(object sender, EventArgs e)
    {
        Trigger(StateEvents.Block);
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
    public const string BlockStart = "OnBlockStart";
    public const string BlockStop = "OnBlockStop";
    public const string Fall = "OnFallingStart";
    public const string Land = "OnFallingStop";
}
