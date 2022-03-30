/******************************************************************************
 * 
 * File: GraphicHandler.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Utility behavior for manipulating Graphics objects.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Utility behavior for manipulating Graphics objects.
/// </summary>
[RequireComponent(typeof(Graphic))]
[DisallowMultipleComponent]
public class GraphicHandler : UIElement
{
    private Graphic _graphic;
    protected Graphic Graphic
    {
        get
        {
            if (!_graphic)
                _graphic = GetComponent<Graphic>();

            return _graphic;
        }
    }

    /// <summary>
    /// Change the color the graphic.
    /// </summary>
    public void SetColor(Color color)
    {
        Graphic.color = color;
    }

    public void CrossFadeAlpha(float alpha, float duration, bool ignoreTimeScale)
    {
        Graphic.CrossFadeAlpha(alpha, duration, ignoreTimeScale);
    }

    public void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha, bool useRGB)
    {
        Graphic.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha, useRGB);
    }

    public void CrossFadeColor(Color targetColor, float duration, bool ignoreTimeScale, bool useAlpha)
    {
        Graphic.CrossFadeColor(targetColor, duration, ignoreTimeScale, useAlpha);
    }
}
