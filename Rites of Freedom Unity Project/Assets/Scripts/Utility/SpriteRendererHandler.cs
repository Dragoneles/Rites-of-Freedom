/******************************************************************************
 * 
 * File: SpriteRendererHandler.cs
 * Author: Joseph Crump
 * Date: 3/21/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Utility component that performs operations on a SpriteRenderer.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility component that performs operations on a <see cref="SpriteRenderer"/>.
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteRendererHandler : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer spriteRenderer
    {
        get
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            return _spriteRenderer;
        }
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    public void FlashColor(Color color, float duration = 0.1f)
    {
        Color originalColor = spriteRenderer.color;

        SetColor(color);

        this.DoAfterDelay(() => SetColor(originalColor), duration);
    }
}
