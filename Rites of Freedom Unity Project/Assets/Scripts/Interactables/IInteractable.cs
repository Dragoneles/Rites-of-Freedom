/******************************************************************************
 * 
 * File: IInteractable.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Interface indicating that an object can be interacted with.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface indicating that an object can be interacted with.
/// </summary>
public interface IInteractable
{
    void Use(IInteractor interactor);
}
