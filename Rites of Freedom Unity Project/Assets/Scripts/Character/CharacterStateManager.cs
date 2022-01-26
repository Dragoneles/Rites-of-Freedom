/******************************************************************************
 * 
 * File: CharacterStateManager.cs
 * Author: Joseph Crump
 * Date: 1/26/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Helper class that sends messages to a character's state machine.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Helper class that sends messages to a character's state machine.
/// </summary>
[RequireComponent(typeof(Character))]
public class CharacterStateManager : MonoBehaviour
{
    private Character character { get; set; }

    private void Awake()
    {
        character = GetComponent<Character>();
    }

    public void Move(MovementEventArgs e)
    {
        Trigger("Move", e);
    }

    public void Stop()
    {
        Trigger("Stop");
    }

    public void Jump()
    {
        Trigger("Jump");
    }

    public void Attack()
    {
        Trigger("Attack");
    }

    public void Block()
    {
        Trigger("Block");
    }

    private void Trigger(string eventName, params object[] args)
    {
        
    }
}
