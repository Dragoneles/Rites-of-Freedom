/******************************************************************************
 * 
 * File: MovementEventArgs.cs
 * Author: Joseph Crump
 * Date: 1/26/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event argument for a move event containing a movement direction.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// Event argument containing a movement direction.
/// </summary>
public class MovementEventArgs : EventArgs
{
    /// <summary>
    /// The 1D axis direction of the movement.
    /// </summary>
    public readonly float Direction;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="direction">
    /// The 1D axis direction of the movement.
    /// </param>
    public MovementEventArgs(float direction)
    {
        Direction = direction;
    }
}
