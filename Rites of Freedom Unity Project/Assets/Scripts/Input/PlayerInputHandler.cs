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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Behavior that logs the state of input events for a character.
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputHandler : VirtualInputHandler
{
    private PlayerInput playerInput { get; set; }

    private bool callbacksRegistered { get; set; } = false;

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

        playerInput ??= GetComponent<PlayerInput>();

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
        playerInput ??= GetComponent<PlayerInput>();

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
        if (context.started) StartCoroutine(Attack.SetDown());
        if (context.canceled) StartCoroutine(Attack.SetUp());
    }

    private void OnBlock(InputAction.CallbackContext context)
    {
        if (context.started) StartCoroutine(Block.SetDown());
        if (context.canceled) StartCoroutine(Block.SetUp());
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) StartCoroutine(Jump.SetDown());
        if (context.canceled) StartCoroutine(Jump.SetUp());
    }

    private void OnRoll(InputAction.CallbackContext context)
    {
        if (context.started) StartCoroutine(Roll.SetDown());
        if (context.canceled) StartCoroutine(Roll.SetUp());
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started) StartCoroutine(Interact.SetDown());
        if (context.canceled) StartCoroutine(Interact.SetUp());
    }

    private void OnHelp(InputAction.CallbackContext context)
    {
        if (context.started) StartCoroutine(Help.SetDown());
        if (context.canceled) StartCoroutine(Help.SetUp());
    }

    private void OnPause(InputAction.CallbackContext context)
    {
        if (context.started) StartCoroutine(Pause.SetDown());
        if (context.canceled) StartCoroutine(Pause.SetUp());
    }
}
