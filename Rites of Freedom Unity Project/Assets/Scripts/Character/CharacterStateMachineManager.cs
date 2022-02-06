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
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Behavior responsible for dispatching events to the
/// Gladiator's State Machines.
/// </summary>
[RequireComponent(typeof(Character))]
public class CharacterStateMachineManager : MonoBehaviour
{
    public class StateEvents
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
        public const string Flinch = "OnFlinchStart";
        public const string FlinchStop = "OnFlinchStop";
        public const string Death = "OnDeath";
    }

    [SerializeField]
    [Tooltip("How long the pending inputs last before expiring.")]
    private float PendingStateDuration = 0.3f;

    public bool ControlsLocked { get; private set; } = false;

    private StateMachineCallbackDictionary CallbackDictionary = 
        new StateMachineCallbackDictionary();

    private StateType NextActionState { get; set; } = StateType.Idle;
    private StateType CurrentActionState { get; set; } = StateType.Idle;

    private Character Character { get; set; }

    /// <summary>
    /// Coroutine that handles the expiration of a pending input.
    /// </summary>
    private Coroutine NextState_Coroutine { get; set; }

    private void Awake()
    {
        Character = GetComponent<Character>();
        Character.GroundStateChanged.AddListener(OnCharacterGroundStateChanged);
        Character.YVelocityChanged.AddListener(OnCharacterYVelocityChanged);
        Character.Flinched.AddListener(OnCharacterFlinched);
        Character.Died.AddListener(OnCharacterDeath);
    }

    /// <summary>
    /// Set the control lock to true. Stop current movement actions.
    /// </summary>
    private void LockControls()
    {
        ControlsLocked = true;
    }

    private void UnlockControls()
    {
        ControlsLocked = false;
    }

    /// <summary>
    /// Switch between movement states based on Direction.
    /// </summary>
    public void Move(float direction)
    {
        if (ControlsLocked || direction == 0f)
        {
            Trigger(StateEvents.MoveStop);
            return;
        }

        Trigger(StateEvents.MoveStart, direction);
    }

    /// <summary>
    /// Enter the Jump state.
    /// </summary>
    public void Jump()
    {
        if (ControlsLocked)
        {
            SetNextState(StateType.Jump);
            return;
        }

        SetCurrentActionState(StateType.Jump);
    }

    /// <summary>
    /// Enter the Attack state.
    /// </summary>
    public void Attack()
    {
        if (ControlsLocked && Character.GetAnimationInt(Character.AnimatorIntegers.AttackCount) < 2)
        {
            SetNextState(StateType.Attack);
            return;
        }

        SetCurrentActionState(StateType.Attack);
    }

    /// <summary>
    /// Enter the Block state.
    /// </summary>
    public void StartBlocking()
    {
        if (CurrentActionState == StateType.Block)
            return;

        if (ControlsLocked)
        {
            SetNextState(StateType.Block);
            return;
        }

        SetCurrentActionState(StateType.Block);
    }

    /// <summary>
    /// Leave the Block state.
    /// </summary>
    public void StopBlocking()
    {
        if (CurrentActionState != StateType.Block)
            return;

        NotifyStateFinished(StateType.Block);
    }

    /// <summary>
    /// Trigger the state machine transition to the flinch state.
    /// </summary>
    public void Flinch()
    {
        SetCurrentActionState(StateType.Flinch);
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
        SetCurrentActionState(NextActionState);

        ClearNextState();
    }

    private void SetCurrentActionState(StateType state)
    {
        CurrentActionState = state;

        LockControls();

        switch (CurrentActionState)
        {
            case StateType.Attack:
                Trigger(StateEvents.Attack);
                break;

            case StateType.Block:
                Trigger(StateEvents.BlockStart);
                break;

            case StateType.Jump:
                Trigger(StateEvents.AttackStop);
                Trigger(StateEvents.BlockStop);
                Trigger(StateEvents.FlinchStop);
                Trigger(StateEvents.Jump);
                UnlockControls();
                break;

            case StateType.Flinch:
                Trigger(StateEvents.Flinch);
                break;

            default:
                Trigger(StateEvents.AttackStop);
                Trigger(StateEvents.BlockStop);
                Trigger(StateEvents.FlinchStop);
                UnlockControls();
                break;
        }
    }

    /// <summary>
    /// Run a state's exit callbacks.
    /// </summary>
    private void InvokeCallbacks(StateType state)
    {
        CallbackDictionary.InvokeCallbacks(state);
    }

    private void Trigger(string eventName, params object[] args)
    {
        CustomEvent.Trigger(gameObject, eventName, args);
    }

    private void ClearNextState()
    {
        SetNextState(StateType.Idle);
    }

    private void SetNextState(StateType state)
    {
        NextActionState = state;
        ResetNextStateCoroutine();
    }

    private void ResetNextStateCoroutine()
    {
        if (NextState_Coroutine != null)
            StopCoroutine(NextState_Coroutine);

        if (NextActionState == StateType.Idle)
            return;

        NextState_Coroutine = StartCoroutine(NextStateCoroutine());
    }

    private IEnumerator NextStateCoroutine()
    {
        yield return new WaitForSeconds(PendingStateDuration);

        ClearNextState();

        NextState_Coroutine = null;
    }

    protected virtual void OnCharacterGroundStateChanged(object sender, BoolEventArgs e)
    {
        if (e == false)
            return;

        Trigger(StateEvents.Land);
    }

    protected virtual void OnCharacterYVelocityChanged(object sender, FloatEventArgs e)
    {
        if (Character.Feet.IsGrounded)
            return;

        if (e < 0f)
            Trigger(StateEvents.Fall);
    }

    protected virtual void OnCharacterFlinched(object sender, EventArgs e)
    {
        Flinch();
    }

    protected virtual void OnCharacterBlocked(object sender, EventArgs e)
    {
        Trigger(StateEvents.Block);
    }

    protected virtual void OnCharacterDeath(object sender, EventArgs e)
    {
        Trigger(StateEvents.Death);
    }
}
