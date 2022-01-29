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
    private Animator animator { get; set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
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
        if (Rigidbody == null)
            return;

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

    public void SetAnimationTrigger(string triggerName)
    {
        if (animator == null)
            return;

        animator.SetTrigger(triggerName);
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
}
