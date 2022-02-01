/******************************************************************************
 * 
 * File: Feetcollider.cs
 * Author: Joseph Crump
 * Date: 1/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior used in conjunction with a collider to detect when a character is 
 *  touching the ground.
 *  
 ******************************************************************************/
using System;
using UnityEngine;

/// <summary>
/// Behavior used in conjunction with a collider to detect when a character is 
/// touching the ground.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class FeetCollider : MonoBehaviour
{
    public event EventHandler<BoolEventArgs> GroundStateChanged;

    public bool IsGrounded { get; private set; } = false;
    private int contacts = 0;

    [SerializeField]
    private LayerMask collisionMask;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & collisionMask) == 0)
            return;

        contacts = Mathf.Max(0, contacts + 1);

        UpdateGroundState();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (((1 << collision.gameObject.layer) & collisionMask) == 0)
            return;

        contacts = Mathf.Max(0, contacts - 1);

        UpdateGroundState();
    }

    private void UpdateGroundState()
    {
        bool previous = IsGrounded;

        IsGrounded = (contacts > 0);

        if (previous == IsGrounded)
            return;

        GroundStateChanged?.Invoke(this, IsGrounded);
    }
}
