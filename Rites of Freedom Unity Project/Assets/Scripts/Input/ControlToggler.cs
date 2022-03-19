/******************************************************************************
 * 
 * File: TriggerRunner.cs
 * Author: Joseph Crump
 * Date: 3/06/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Basic component that toggles a gameobject on or off based on a held input.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Basic component that toggles a gameobject on or off based on a held input.
/// </summary>
public class ControlToggler : MonoBehaviour
{
    [SerializeField]
    private GameObject toggledObject;

    public void Toggle(InputAction.CallbackContext context)
    {
        if (!toggledObject)
            return;

        if (context.started)
            toggledObject.SetActive(true);

        if (context.canceled)
            toggledObject.SetActive(false);
    }
}
