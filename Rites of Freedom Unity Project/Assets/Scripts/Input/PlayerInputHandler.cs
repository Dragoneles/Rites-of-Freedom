/******************************************************************************
 * 
 * File: CharacterInputs.cs
 * Author: Joseph Crump
 * Date: 3/21/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that logs the state of input events for a character.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Behavior that logs the state of input events for a character.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : VirtualInputHandler
{
    private PlayerInput playerInput;

    private bool callbacksRegistered = false;

    private void Start()
    {
        EnableInputs();
    }

    private void Update()
    {
        if (!callbacksRegistered)
            EnableInputs();
    }

    private void OnDisable()
    {
        DisableInputs();
    }

    private void EnableInputs()
    {
        if (callbacksRegistered)
            return;

        if (!playerInput)
            playerInput = GetComponent<PlayerInput>();

        RegisterInputAction(playerInput.actions["Move"], OnMove);
        RegisterInputAction(playerInput.actions["Attack"], OnAttack);
        RegisterInputAction(playerInput.actions["Block"], OnBlock);
        RegisterInputAction(playerInput.actions["Jump"], OnJump);
        RegisterInputAction(playerInput.actions["Roll"], OnRoll);
        RegisterInputAction(playerInput.actions["Interact"], OnInteract);
        RegisterInputAction(playerInput.actions["Help"], OnHelp);
        RegisterInputAction(playerInput.actions["Pause"], OnPause);

        callbacksRegistered = true;
    }

    private void DisableInputs()
    {
        if (!playerInput)
            playerInput = GetComponent<PlayerInput>();

        UnregisterInputAction(playerInput.actions["Move"], OnMove);
        UnregisterInputAction(playerInput.actions["Attack"], OnAttack);
        UnregisterInputAction(playerInput.actions["Block"], OnBlock);
        UnregisterInputAction(playerInput.actions["Jump"], OnJump);
        UnregisterInputAction(playerInput.actions["Roll"], OnRoll);
        UnregisterInputAction(playerInput.actions["Interact"], OnInteract);
        UnregisterInputAction(playerInput.actions["Help"], OnHelp);
        UnregisterInputAction(playerInput.actions["Pause"], OnPause);

        callbacksRegistered = false;
    }

    private void RegisterInputAction(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.started += callback;
        action.performed += callback;
        action.canceled += callback;
    }

    private void UnregisterInputAction(InputAction action, Action<InputAction.CallbackContext> callback)
    {
        action.started -= callback;
        action.performed -= callback;
        action.canceled -= callback;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        MovementAxis.SetAxisValue(context.ReadValue<float>());
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started) Attack.SetDown(coroutineRunner: this);
        if (context.canceled) Attack.SetUp(coroutineRunner: this);
    }

    private void OnBlock(InputAction.CallbackContext context)
    {
        if (context.started) Block.SetDown(coroutineRunner: this);
        if (context.canceled) Block.SetUp(coroutineRunner: this);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) Jump.SetDown(coroutineRunner: this);
        if (context.canceled) Jump.SetUp(coroutineRunner: this);
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started) Roll.SetDown(coroutineRunner: this);
        if (context.canceled) Roll.SetUp(coroutineRunner: this);
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started) Interact.SetDown(coroutineRunner: this);
        if (context.canceled) Interact.SetUp(coroutineRunner: this);
    }

    private void OnHelp(InputAction.CallbackContext context)
    {
        if (context.started) Help.SetDown(coroutineRunner: this);
        if (context.canceled) Help.SetUp(coroutineRunner: this);
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (context.started) Pause.SetDown(coroutineRunner: this);
        if (context.canceled) Pause.SetUp(coroutineRunner: this);
    }
}
