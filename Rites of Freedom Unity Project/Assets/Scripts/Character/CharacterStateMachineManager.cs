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
using UnityEngine.InputSystem;

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
        public const string Roll = "OnRoll";
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
    private float InputMemoryDuration = 0.3f;

    public bool InputLocked { get; private set; } = false;

    private StateMachineCallbackDictionary CallbackDictionary = 
        new StateMachineCallbackDictionary();

    public StateType NextActionState { get; private set; } = StateType.Idle;
    public StateType CurrentActionState { get; private set; } = StateType.Idle;

    private Character Character { get; set; }

    /// <summary>
    /// Coroutine that handles the expiration of a pending input.
    /// </summary>
    private Coroutine InputMemoryCoroutineInstance { get; set; }

    private Coroutine InputLockCoroutineInstance { get; set; }

    private void Awake()
    {
        Character = GetComponent<Character>();

        Character.GroundStateChanged.AddListener(OnCharacterGroundStateChanged);
        Character.YVelocityChanged.AddListener(OnCharacterYVelocityChanged);
        Character.Flinched.AddListener(OnCharacterFlinched);
        Character.Blocked.AddListener(OnCharacterBlocked);
        Character.Died.AddListener(OnCharacterDeath);
    }

    /// <summary>
    /// Switch between movement states based on Direction.
    /// </summary>
    public void Move(float direction)
    {
        if (InputLocked || direction == 0f)
        {
            Trigger(StateEvents.MoveStop);
            return;
        }

        Trigger(StateEvents.MoveStart, direction);
    }

    /// <summary>
    /// Instruct the state machine to stop moving.
    /// </summary>
    public void StopMoving()
    {
        Move(0f);
    }

    /// <summary>
    /// Enter the Jump state.
    /// </summary>
    public void Jump()
    {
        if (InputLocked)
        {
            SetNextState(StateType.Jump);
            return;
        }

        SetCurrentActionState(StateType.Jump);
    }

    /// <summary>
    /// Enter the Roll state.
    /// </summary>
    public void Roll(float direction)
    {
        if (InputLocked)
        {
            SetNextState(StateType.Roll);
            return;
        }

        CallbackDictionary.AddCallback(StateType.Roll, UnlockInputs);

        SetCurrentActionState(StateType.Roll);
    }

    /// <summary>
    /// Enter the Attack state.
    /// </summary>
    public void Attack()
    {
        if (InputLocked && Character.GetAnimationInt(Character.AnimatorIntegers.AttackCount) < 2)
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

        if (InputLocked)
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
        ClearNextState();
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

    /// <summary>
    /// Set the control lock to true. Stop current movement actions.
    /// </summary>
    public void LockInputs(float lockDuration)
    {
        StopInputLockCoroutine();

        InputLockCoroutineInstance = StartCoroutine(InputLockCoroutine(lockDuration));
    }

    private void UnlockInputs()
    {
        StopInputLockCoroutine();

        InputLocked = false;
    }

    private void SetCurrentActionState(StateType state)
    {
        CurrentActionState = state;

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
                Trigger(StateEvents.Jump);
                break;

            case StateType.Flinch:
                Trigger(StateEvents.BlockStop);
                Trigger(StateEvents.MoveStop);
                Trigger(StateEvents.Flinch);
                break;

            case StateType.Roll:
                Trigger(StateEvents.AttackStop);
                Trigger(StateEvents.BlockStop);
                Trigger(StateEvents.Roll);
                break;

            default:
                Trigger(StateEvents.AttackStop);
                Trigger(StateEvents.BlockStop);
                Trigger(StateEvents.FlinchStop);
                UnlockInputs();
                break;
        }
    }

    private void RunPendingInput()
    {
        SetCurrentActionState(NextActionState);

        ClearNextState();
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
        if (InputMemoryCoroutineInstance != null)
            StopCoroutine(InputMemoryCoroutineInstance);

        if (NextActionState == StateType.Idle)
            return;

        InputMemoryCoroutineInstance = StartCoroutine(NextStateCoroutine());
    }

    private void StopInputLockCoroutine()
    {
        if (InputLockCoroutineInstance != null)
        {
            StopCoroutine(InputLockCoroutineInstance);
            InputLockCoroutineInstance = null;
        }
    }

    private void DisableInputBehaviors()
    {
        DisablePlayerInput();
        DisableAIBehaviorTree();
    }

    private void DisableAIBehaviorTree()
    {
        var behaviorTree = GetComponentInChildren<EnemyBehaviorTree>();
        if (behaviorTree)
            behaviorTree.enabled = false;
    }

    private void DisablePlayerInput()
    {
        var playerInput = GetComponentInChildren<PlayerInput>();
        if (playerInput)
            playerInput.enabled = false;
    }

    private IEnumerator InputLockCoroutine(float lockDuration)
    {
        InputLocked = true;

        yield return new WaitForSeconds(lockDuration);

        UnlockInputs();
    }

    private IEnumerator NextStateCoroutine()
    {
        yield return new WaitForSeconds(InputMemoryDuration);

        ClearNextState();

        InputMemoryCoroutineInstance = null;
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

        DisableInputBehaviors();
    }
}
