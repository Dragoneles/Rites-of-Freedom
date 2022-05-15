/******************************************************************************
 * 
 * File: InstantFillMethod.cs
 * Author: Joseph Crump
 * Date: 2/14/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Image fill method that sets a image's fill amount instantly.
 *  
 ******************************************************************************/
using UnityEngine.UI;

/// <summary>
/// Image fill method that sets a image's fill amount instantly.
/// </summary>
public class InstantFillMethod : FillMethod
{
    public override void SetFillAmount(Image image, float fillAmount)
    {
        image.fillAmount = fillAmount;
    }
}
