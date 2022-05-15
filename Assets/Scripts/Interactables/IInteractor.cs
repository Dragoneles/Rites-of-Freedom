/******************************************************************************
 * 
 * File: IInteractor.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Interface flagging an object as something that can use an IInteractable.
 *  
 ******************************************************************************/

/// <summary>
/// Interface flagging an object as something that can use an IInteractable.
/// </summary>
public interface IInteractor
{
    /// <summary>
    /// Interact with an interactable object.
    /// </summary>
    void Interact(IInteractable interactable);
}
