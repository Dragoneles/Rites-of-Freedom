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
using System.Collections;
using System.Collections.Generic;
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

    private void Awake()
    {
        stateMachine = GetComponent<CharacterStateMachineManager>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled)
            stateMachine.Stop();

        if (context.started)
            stateMachine.Move(new MovementEventArgs(context.ReadValue<float>()));
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
            stateMachine.Jump();
    }

    public void Block(InputAction.CallbackContext context)
    {
        if (context.performed)
            stateMachine.Block();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            stateMachine.Attack();
    }

    public void Dash(InputAction.CallbackContext context)
    {

    }
}
