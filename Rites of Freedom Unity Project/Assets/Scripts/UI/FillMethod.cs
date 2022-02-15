/******************************************************************************
 * 
 * File: FillMethod.cs
 * Author: Joseph Crump
 * Date: 2/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object determining how to set an image's fill amount.
 *  
 ******************************************************************************/
using UnityEngine.UI;

/// <summary>
/// Action determining how to set an image's fill amount.
/// </summary>
[System.Serializable]
public abstract class FillMethod
{
    /// <summary>
    /// Set an image's fill amount.
    /// </summary>
    /// <param name="image">
    /// The image that is modified.
    /// </param>
    /// <param name="fillAmount">
    /// The image's new fill value.
    /// </param>
    public abstract void SetFillAmount(Image image, float fillAmount);
}
