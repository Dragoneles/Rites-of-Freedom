/******************************************************************************
 * 
 * File: SideScrollerCamera.cs
 * Author: Joseph Crump
 * Date: 2/05/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Camera that maintains a fixed y-height and anchors to objects on the 
 *  x-axis.
 *  
 ******************************************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Camera that maintains a fixed y-height and anchors to objects on the 
/// x - axis.
/// </summary>
public class SideScrollerCamera : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Smoothing factor used for camera interpolation.")]
    private float SmoothTime = 0.2f;

    [SerializeField]
    [Tooltip("Objects that are followed by the camera.")]
    private List<CameraAnchor> Anchors = new List<CameraAnchor>();

    private float yPosition { get; set; } = 0f;
    private float zPosition { get; set; } = 0f;

    private float targetX = 0f;
    private float velocity = 0f;

    private void Awake()
    {
        yPosition = transform.position.y;
        zPosition = transform.position.z;
    }

    private void LateUpdate()
    {
        targetX = CalculateAnchorCenter();

        float currentX = transform.position.x;

        float newX = Mathf.SmoothDamp(currentX, targetX, ref velocity, SmoothTime);

        transform.position = new Vector3(newX, yPosition, zPosition);
    }

    /// <summary>
    /// Add a new camera target.
    /// </summary>
    public void AddAnchor(CameraAnchor anchor)
    {
        if (Anchors.Contains(anchor))
            return;

        Anchors.Add(anchor);
        anchor.Disabled += OnAnchorDisabled;
    }

    /// <summary>
    /// Remove a camera target from the camera.
    /// </summary>
    public void RemoveAnchor(CameraAnchor anchor)
    {
        if (!Anchors.Contains(anchor))
            return;

        Anchors.Remove(anchor);
        anchor.Disabled -= OnAnchorDisabled;
    }

    /// <summary>
    /// Average the X positions and weights of all anchors.
    /// </summary>
    private float CalculateAnchorCenter()
    {
        float sumWeight = GetSumOfAnchorWeights();

        float xSum = 0f;
        foreach (CameraAnchor anchor in Anchors)
        {
            xSum += anchor.Position.x;
        }

        float center = xSum / Anchors.Count;

        float centerDisplacement = 0f;

        foreach (CameraAnchor anchor in Anchors)
        {
            float distanceFromCenter = anchor.Position.x - center;
            centerDisplacement += (distanceFromCenter * (anchor.GetWeight() / sumWeight));
        }

        center += centerDisplacement;
        return center;
    }

    private float GetSumOfAnchorWeights()
    {
        float sum = 0f;
        foreach (CameraAnchor anchor in Anchors)
        {
            sum += anchor.GetWeight();
        }

        return sum;
    }

    protected virtual void OnAnchorDisabled(object sender, EventArgs e)
    {
        CameraAnchor anchor = sender as CameraAnchor;

        if (anchor == null)
            return;

        RemoveAnchor(anchor);
    }
}
