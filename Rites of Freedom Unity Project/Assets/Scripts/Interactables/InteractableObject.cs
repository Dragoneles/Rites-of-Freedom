/******************************************************************************
 * 
 * File: InteractableObject.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Default interactable object.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Default interactable object.
/// </summary>
public class InteractableObject : MonoBehaviour, IInteractable
{
    public UnityEvent<InteractEventArgs> Used = new UnityEvent<InteractEventArgs>();

    public void Use(IInteractor interactor)
    {
        OnUse();

        Used?.Invoke(new InteractEventArgs(interactor, this));
    }

    protected virtual void OnUse() { }
}
