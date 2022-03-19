/******************************************************************************
 * 
 * File: AIInput.cs
 * Author: Joseph Crump
 * Date: 3/15/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Virtual input behavior for the AI, counterpart to PlayerInput.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Virtual input behavior for the AI, counterpart to PlayerInput.
/// </summary>
public class AIInput : MonoBehaviour
{
    private Gamepad virtualInputDevice { get; set; }

    private void Awake()
    {
        virtualInputDevice = new Gamepad();
        
        InputSystem.AddDevice(virtualInputDevice);
    }

    private void OnDestroy()
    {
        if (virtualInputDevice == null)
            return;

        InputSystem.RemoveDevice(virtualInputDevice);
    }
}
