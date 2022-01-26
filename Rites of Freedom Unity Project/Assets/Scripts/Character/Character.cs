/******************************************************************************
 * 
 * File: Character.cs
 * Author: Joseph Crump
 * Date: 1/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event dispatcher behavior for a playable character's state machine.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event dispatcher behavior for a playable character's state machine.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    public float MoveSpeed = 4f;
    public float JumpVelocity = 5f;

    public Rigidbody2D Rigidbody { get; private set; }
    private FeetCollider feetCollider { get; set; }

    /// <summary>
    /// Returns true if the character is on the ground.
    /// </summary>
    public bool IsGrounded
    {
        get => feetCollider.IsGrounded;
    }

    /// <summary>
    /// Returns true if the character is not grounded and has a negative 
    /// y velocity.
    /// </summary>
    public bool IsFalling
    {
        get => !feetCollider.IsGrounded && Rigidbody.velocity.y.IsNegative();
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponentInChildren<FeetCollider>();
    }

    public void Move(float direction)
    {
        Vector2 directionVector = new Vector2(direction, 0f);
        Vector2 velocity = directionVector * MoveSpeed;
        velocity.y = Rigidbody.velocity.y;

        Rigidbody.velocity = velocity;
    }

    public void Stop()
    {
        Vector2 velocity = Rigidbody.velocity;
        velocity.x = 0f;

        Rigidbody.velocity = velocity;
    }

    public void Attack()
    {

    }

    public void Block()
    {

    }

    public void Jump()
    {

    }
}
