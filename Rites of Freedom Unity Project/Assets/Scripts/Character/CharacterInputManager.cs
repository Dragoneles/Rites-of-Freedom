/******************************************************************************
 * 
 * File: CharacterInputManager.cs
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
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Behavior responsible for dispatching events to the
/// Gladiator's State Machine.
/// </summary>
public class CharacterInputManager : MonoBehaviour
{
    [SerializeField]
    private CharacterStateManager characterMessager;

    public void Attack(InputAction.CallbackContext context)
    {
        characterMessager.Attack();
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            characterMessager.Stop();
            return;
        }

        float direction = context.ReadValue<float>();
        var eventArgs = new MovementEventArgs(direction);

        characterMessager.Move(eventArgs);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        characterMessager.Jump();
    }

    public void Block(InputAction.CallbackContext context)
    {
        characterMessager.Block();
    }
}
