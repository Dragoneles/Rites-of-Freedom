/******************************************************************************
 * 
 * File: CharacterInputManager.cs
 * Author: Joseph Crump
 * Date: 1/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Component that receives inputs from the controller and sends the input to
 *  the character's state machine manager.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Component that receives inputs from the controller and sends the input to
/// the character 's state machine manager.
/// </summary>
[RequireComponent(typeof(CharacterStateMachineManager))]
public class CharacterInputManager : MonoBehaviour
{
    private CharacterStateMachineManager StateMachine { get; set; }

    [SerializeField]
    private ActionTypes allowedActions = ActionTypes.Story;

    private float moveDirection { get; set; } = 0f;
    private Coroutine move_Coroutine { get; set; }
    private Coroutine roll_Coroutine { get; set; }
    private Coroutine jump_Coroutine { get; set; }
    private Coroutine attack_Coroutine { get; set; }
    private Coroutine block_Coroutine { get; set; }

    private void Awake()
    {
        StateMachine = GetComponent<CharacterStateMachineManager>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!allowedActions.HasFlag(ActionTypes.Move))
            return;

        if (context.canceled)
        {
            moveDirection = 0f;
            return;
        }

        if (context.performed)
        {
            moveDirection = context.ReadValue<float>();

            move_Coroutine ??= StartCoroutine(InputCoroutine(
                () => StateMachine.Move(moveDirection)));
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (!allowedActions.HasFlag(ActionTypes.Jump))
            return;

        if (context.canceled)
        {
            Stop(jump_Coroutine);
            jump_Coroutine = null;
            return;
        }

        if (context.started)
            StateMachine.Jump();

        if (context.performed)
            jump_Coroutine ??= StartCoroutine(InputCoroutine(StateMachine.Jump));
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (!allowedActions.HasFlag(ActionTypes.Block))
            return;

        if (context.canceled)
        {
            Stop(block_Coroutine);
            block_Coroutine = null;
            StateMachine.StopBlocking();
            return;
        }

        if (context.started)
            StateMachine.StartBlocking();

        if (context.performed)
            block_Coroutine ??= StartCoroutine(InputCoroutine(StateMachine.StartBlocking));
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!allowedActions.HasFlag(ActionTypes.Attack))
            return;

        if (context.canceled)
        {
            Stop(attack_Coroutine);
            attack_Coroutine = null;
            return;
        }

        if (context.started)
            StateMachine.Attack();

        if (context.performed)
            attack_Coroutine ??= StartCoroutine(InputCoroutine(StateMachine.Attack));
    }

    public void Roll(InputAction.CallbackContext context)
    {
        if (!allowedActions.HasFlag(ActionTypes.Roll))
            return;

        if (context.canceled)
        {
            Stop(roll_Coroutine);
            roll_Coroutine = null;
            return;
        }

        if (context.started)
            StateMachine.Roll();

        if (context.performed)
        {
            roll_Coroutine ??= StartCoroutine(InputCoroutine(
                () => StateMachine.Roll()));
        }
    }

    private void Stop(Coroutine coroutine)
    {
        if (coroutine == null)
            return;

        StopCoroutine(coroutine);
    }

    private IEnumerator InputCoroutine(Action loopOperation)
    {
        while (enabled)
        {
            yield return new WaitForEndOfFrame();

            loopOperation.Invoke();
        }
    }
}
