/******************************************************************************
 * 
 * File: CollisionEventArgs.cs
 * Author: Joseph Crump
 * Date: 1/23/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event arguments for a collision event.
 *  
 ******************************************************************************/
using System;
using UnityEngine;

/// <summary>
/// EventArgs for a collision event.
/// </summary>
public class CollisionEventArgs : EventArgs
{
    /// <summary>
    /// The Collider2D that was collided with.
    /// </summary>
    public readonly Collider2D Collider;
    public GameObject GameObject => Collider.gameObject;

    /// <param name="collider">
    /// The Collider2D that was collided with.
    /// </param>
    public CollisionEventArgs(Collider2D collider)
    {
        Collider = collider;
    }
}
