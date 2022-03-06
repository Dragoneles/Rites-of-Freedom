/******************************************************************************
 * 
 * File: InteractEventArgs.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event arguments for interactable events.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// Event arguments for interactable events.
/// </summary>
public class InteractEventArgs : EventArgs
{
    /// <summary>
    /// The interactor triggering the event.
    /// </summary>
    public readonly IInteractor Interactor;

    /// <summary>
    /// The interactable object triggering the event.
    /// </summary>
    public readonly IInteractable Interactable;

    public InteractEventArgs(IInteractor interactor, IInteractable usedObject)
    {
        Interactor = interactor;
        Interactable = usedObject;
    }
}
