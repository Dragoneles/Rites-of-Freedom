/******************************************************************************
 * 
 * File: QuitFacilitator.cs
 * Author: Joseph Crump
 * Date: 4/09/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that allows the application to exit.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Behavior that allows the application to exit.
/// </summary>
public class QuitFacilitator : MonoBehaviour
{
    /// <summary>
    /// Close the application.
    /// </summary>
    public void Quit()
    {
        Application.Quit(0);
    }
}
