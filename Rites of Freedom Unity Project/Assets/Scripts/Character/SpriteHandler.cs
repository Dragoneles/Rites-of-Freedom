/******************************************************************************
 * 
 * File: SpriteHandler.cs
 * Author: Joseph Crump
 * Date: 3/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Script handling orientation of an object, used to manipulate a character's 
 *  facing direction without inverting the root object.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script handling orientation of an object, used to manipulate a character's 
/// facing direction without inverting the root object.
/// </summary>
public class SpriteHandler : MonoBehaviour
{
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
    /// Get the direction that the player is currently facing.
    /// </summary>
    public float GetFacingDirection()
    {
        float right = 1f;
        float left = -1f;

        if (transform.localScale.x.IsPositive())
            return right;

        return left;
    }

    /// <summary>
    /// Tell the character to face a specified direction.
    /// </summary>
    public void FaceDirection(float direction)
    {
        direction = Mathf.Clamp(direction, Direction.Left, Direction.Right);
        direction = Mathf.Round(direction);

        if (direction == 0f)
            return;

        float x = Mathf.Abs(transform.localScale.x);
        float y = transform.localScale.y;
        float z = transform.localScale.z;
        transform.localScale = new Vector3(x * direction, y, z);
    }

    /// <summary>
    /// Set this character's local X scale to face left.
    /// </summary>
    public void FaceLeft()
    {
        FaceDirection(Direction.Left);
    }

    /// <summary>
    /// Set this character's local X scale to face right.
    /// </summary>
    public void FaceRight()
    {
        FaceDirection(Direction.Right);
    }

    /// <summary>
    /// Returns true if the character is left of the specified point.
    /// </summary>
    public bool IsLeftOfPoint(Vector2 point)
    {
        return IsLeftOfPoint(point.x);
    }

    /// <summary>
    /// Returns true if the character is left of the specified point.
    /// </summary>
    public bool IsLeftOfPoint(float point)
    {
        return transform.position.x < point;
    }

    /// <summary>
    /// Returns true if the character is left of the specified point.
    /// </summary>
    public bool IsRightOfPoint(Vector2 point)
    {
        return IsRightOfPoint(point.x);
    }

    /// <summary>
    /// Returns true if the character is left of the specified point.
    /// </summary>
    public bool IsRightOfPoint(float point)
    {
        return transform.position.x > point;
    }

    /// <summary>
    /// Returns true if the character is facing the specified point.
    /// </summary>
    public bool IsFacingPoint(Vector2 point)
    {
        return IsFacingPoint(point.x);
    }

    /// <summary>
    /// Returns true if the character is facing the specified point.
    /// </summary>
    public bool IsFacingPoint(float point)
    {
        return IsLeftOfPoint(point) == transform.localScale.x.IsPositive();
    }

    /// <summary>
    /// Flip the character's facing direction to face the specified point.
    /// </summary>
    public void FacePoint(Vector2 point)
    {
        FacePoint(point.x);
    }

    /// <summary>
    /// Flip the character's facing direction to face the specified point.
    /// </summary>
    public void FacePoint(float point)
    {
        if (IsLeftOfPoint(point))
            FaceRight();
        else if (IsRightOfPoint(point))
            FaceLeft();
    }
}
