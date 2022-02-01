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
    private CharacterStateMachineManager stateMachine { get; set; }

    private float moveDirection { get; set; } = 0f;
    private Coroutine moveCoroutine { get; set; }
    private Coroutine jumpCoroutine { get; set; }
    private Coroutine attackCoroutine { get; set; }
    private Coroutine blockCoroutine { get; set; }

    private void Awake()
    {
        stateMachine = GetComponent<CharacterStateMachineManager>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            moveDirection = 0f;
            return;
        }

        if (context.performed)
        {
            moveDirection = context.ReadValue<float>();

            moveCoroutine ??= StartCoroutine(InputCoroutine(
                () => stateMachine.Move(moveDirection)));
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Stop(jumpCoroutine);
            jumpCoroutine = null;
            return;
        }

        if (context.started)
            stateMachine.Jump();

        if (context.performed)
            jumpCoroutine ??= StartCoroutine(InputCoroutine(stateMachine.Jump));
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Stop(blockCoroutine);
            blockCoroutine = null;
            stateMachine.BlockStop();
            return;
        }

        if (context.started)
            stateMachine.BlockStart();

        if (context.performed)
            blockCoroutine ??= StartCoroutine(InputCoroutine(stateMachine.BlockStart));
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            Stop(attackCoroutine);
            attackCoroutine = null;
            return;
        }

        if (context.started)
            stateMachine.Attack();

        if (context.performed)
            attackCoroutine ??= StartCoroutine(InputCoroutine(stateMachine.Attack));
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
