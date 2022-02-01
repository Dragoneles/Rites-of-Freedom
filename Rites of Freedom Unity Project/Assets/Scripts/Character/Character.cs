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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Event dispatcher behavior for a playable character's state machine.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IAttackable
{
    public event EventHandler<FloatEventArgs> YVelocityChanged;
    public event EventHandler<BoolEventArgs> GroundStateChanged;
    public event EventHandler<AttackEventArgs> Blocked;
    public event EventHandler<AttackEventArgs> Attacked;

    public Vital Health = new Vital(25);
    public Stat MoveSpeed =  new Stat(4);
    public Stat JumpVelocity = new Stat(5);

    public Rigidbody2D Rigidbody { get; private set; }
    public FeetCollider Feet { get; private set; }
    public Animator Animator { get; private set; }

    private float yVelocity { get; set; } = 0f;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        Feet = GetComponentInChildren<FeetCollider>();

        Feet.GroundStateChanged += OnFeetGroundStateChanged;
    }

    private void Update()
    {
        CheckYVelocity();
    }

    /// <summary>
    /// Set the character's x velocity based on an input direction.
    /// </summary>
    public void Move(float direction)
    {
        Vector2 directionVector = new Vector2(direction, 0f);
        Vector2 velocity = directionVector * MoveSpeed;
        velocity.y = Rigidbody.velocity.y;

        Rigidbody.velocity = velocity;
    }

    /// <summary>
    /// Halt the character's x velocity.
    /// </summary>
    public void Stop()
    {
        if (Rigidbody == null)
            return;

        if (!Feet.IsGrounded)
            return;

        Vector2 velocity = Rigidbody.velocity;
        velocity.x = 0f;

        Rigidbody.velocity = velocity;
    }

    public void ReceiveAttack(AttackInstance attack)
    {
        
    }

    /// <summary>
    /// Apply an upwards y-velocity to the player.
    /// </summary>
    public void Jump()
    {
        Vector2 velocity = Rigidbody.velocity;
        float x = velocity.x;
        Rigidbody.velocity = new Vector2(x, JumpVelocity);
    }

    public void SetAnimationTrigger(string triggerName)
    {
        if (Animator == null)
            return;

        Animator.SetTrigger(triggerName);
    }

    public void SetAnimationBool(string boolName, bool value)
    {
        Animator.SetBool(boolName, value);
    }

    public bool GetAnimationBool(string boolName)
    {
        return Animator.GetBool(boolName);
    }

    public void SetAnimationInt(string intName, int value)
    {
        if (Animator == null)
            return;

        Animator.SetInteger(intName, value);
    }

    public int GetAnimationInt(string intName)
    {
        if (Animator == null)
            return 0;

        return Animator.GetInteger(intName);
    }

    /// <summary>
    /// Flip this character's transform in the X-dimension.
    /// </summary>
    public void Flip()
    {
        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(-x, y, z);
    }

    /// <summary>
    /// Set this character's local X scale to face left.
    /// </summary>
    public void FaceLeft()
    {
        float x = Mathf.Abs(transform.localScale.x);
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(-x, y, z);
    }

    /// <summary>
    /// Set this character's local X scale to face right.
    /// </summary>
    public void FaceRight()
    {
        float x = Mathf.Abs(transform.localScale.x);
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(x, y, z);
    }

    private void TakeDamage(float amount)
    {
        
    }

    private void CheckYVelocity()
    {
        float previousVelocity = yVelocity;
        float currentVelocity = Rigidbody.velocity.y;

        if (previousVelocity == currentVelocity)
            return;

        yVelocity = currentVelocity;

        if (previousVelocity.IsPositive() != currentVelocity.IsPositive())
            YVelocityChanged?.Invoke(this, yVelocity);
    }

    protected virtual void OnFeetGroundStateChanged(object sender, BoolEventArgs e)
    {
        GroundStateChanged?.Invoke(this, e);
    }
}
