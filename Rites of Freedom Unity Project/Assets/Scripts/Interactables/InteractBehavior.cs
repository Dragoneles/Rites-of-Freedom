/******************************************************************************
 * 
 * File: InteractBehavior.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that can interact with objects it collides with.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Behavior that can interact with objects it collides with.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class InteractBehavior : MonoBehaviour, IInteractor
{
    public UnityEvent<InteractEventArgs> FoundInteractable = new UnityEvent<InteractEventArgs>();
    public UnityEvent<InteractEventArgs> LostInteractable = new UnityEvent<InteractEventArgs>();
    public UnityEvent<InteractEventArgs> UsedInteractable = new UnityEvent<InteractEventArgs>();

    private IInteractable interactable { get; set; }

    /// <summary>
    /// Use the active interactable object. Does nothing if 
    /// no interactable object is nearby.
    /// </summary>
    public void Interact()
    {
        if (interactable == null)
            return;

        Interact(interactable);
    }

    public void Interact(IInteractable interactable)
    {
        IInteractor interactor = this;

        interactable.Use(interactor);

        UsedInteractable?.Invoke(new InteractEventArgs(this, interactable));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (interactable == null)
            return;

        SetInteractable(interactable);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();

        if (this.interactable == interactable)
            SetInteractable(null);
    }

    private void SetInteractable(IInteractable interactable)
    {
        if (this.interactable == interactable)
            return;

        if (this.interactable != null)
            LostInteractable?.Invoke(new InteractEventArgs(this, this.interactable));

        this.interactable = interactable;

        if (this.interactable != null)
            FoundInteractable?.Invoke(new InteractEventArgs(this, interactable));
    }
}
