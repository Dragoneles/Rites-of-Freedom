/******************************************************************************
 * 
 * File: CameraTarget.cs
 * Author: Joseph Crump
 * Date: 2/05/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object that can be tracked by the SideScrollerCamera.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object that can be tracked by the SideScrollerCamera.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class CameraAnchor : MonoBehaviour
{
    public event EventHandler Disabled;

    public Vector2 Position => transform.position;

    [SerializeField]
    [Tooltip("Whether or not this target can be removed from the camera.")]
    private bool CanBeRemoved = true;

    [SerializeField]
    [Tooltip("The amount of pull that this target has on the camera.")]
    private float Weight = 1f;

    public float GetWeight() => Weight;

    private void OnDisable()
    {
        Disabled?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var camera = collision.GetComponent<SideScrollerCamera>();

        if (camera != null)
            camera.AddAnchor(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (CanBeRemoved == false)
            return;

        var camera = collision.GetComponent<SideScrollerCamera>();

        if (camera != null)
            camera.RemoveAnchor(this);
    }
}
