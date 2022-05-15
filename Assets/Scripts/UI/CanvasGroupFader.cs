/******************************************************************************
 * 
 * File: CanvasGroupFader.cs
 * Author: Joseph Crump
 * Date: 4/04/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Series of events that execute when the scene loads.
 *  
 ******************************************************************************/
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Series of events that execute when the scene loads.
/// </summary>
[RequireComponent(typeof(CanvasGroup))]
public class CanvasGroupFader : MonoBehaviour
{
    [SerializeField] 
    private CanvasGroup fadeCanvas;

    [SerializeField] 
    private float canvasFadeTime = 1f;

    private void OnValidate()
    {
        fadeCanvas = GetComponent<CanvasGroup>();
    }

    /// <summary>
    /// Fade the canvas to the target value.
    /// </summary>
    public void FadeCanvas(float targetAlpha)
    {
        if (fadeCanvas == null)
            return;

        var fader = fadeCanvas.DOFade(targetAlpha, canvasFadeTime);

        if (PauseFacilitator.IsPaused)
            fader.SetUpdate(isIndependentUpdate: true);
    }

    /// <summary>
    /// Change the time it takes the canvas to fade.
    /// </summary>
    public void SetFadeTime(float seconds)
    {
        canvasFadeTime = seconds;
    }

    /// <summary>
    /// Get the value of the <see cref="canvasFadeTime"/> field.
    /// </summary>
    public float GetFadeTime()
    {
        return canvasFadeTime;
    }

    /// <summary>
    /// Set the alpha of the canvas group instantly.
    /// </summary>
    public void SetAlpha(float value)
    {
        fadeCanvas.alpha = value;
    }
}
