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

    private float MoveDirection { get; set; } = 0f;
    private Coroutine Move_Coroutine { get; set; }
    private Coroutine Jump_Coroutine { get; set; }
    private Coroutine Attack_Coroutine { get; set; }
    private Coroutine Block_Coroutine { get; set; }

    private void Awake()
    {
        StateMachine = GetComponent<CharacterStateMachineManager>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            MoveDirection = 0f;
            return;
        }

        if (context.performed)
        {
            MoveDirection = context.ReadValue<float>();

            Move_Coroutine ??= StartCoroutine(InputCoroutine(
                () => StateMachine.Move(MoveDirection)));
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Stop(Jump_Coroutine);
            Jump_Coroutine = null;
            return;
        }

        if (context.started)
            StateMachine.Jump();

        if (context.performed)
            Jump_Coroutine ??= StartCoroutine(InputCoroutine(StateMachine.Jump));
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Stop(Block_Coroutine);
            Block_Coroutine = null;
            StateMachine.StopBlocking();
            return;
        }

        if (context.started)
            StateMachine.StartBlocking();

        if (context.performed)
            Block_Coroutine ??= StartCoroutine(InputCoroutine(StateMachine.StartBlocking));
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Stop(Attack_Coroutine);
            Attack_Coroutine = null;
            return;
        }

        if (context.started)
            StateMachine.Attack();

        if (context.performed)
            Attack_Coroutine ??= StartCoroutine(InputCoroutine(StateMachine.Attack));
    }

    public void Dash(InputAction.CallbackContext context)
    {

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
