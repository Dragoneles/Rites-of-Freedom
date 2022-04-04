/******************************************************************************
 * 
 * File: EnterRegionEventCallback.cs
 * Author: Joseph Crump
 * Date: 4/03/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Callback for an object entering a region.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Callback for an object entering a region.
/// </summary>
public class EnterRegionEventCallback : EventCallback<CollisionEventArgs>
{
    [SerializeField] private Character triggeringCharacter;
    [SerializeField] private Region triggeringRegion;

    protected override UnityEvent<CollisionEventArgs> Event
    {
        get => triggeringRegion.ObjectEntered;
    }

    protected override bool ValidateCallback(CollisionEventArgs args)
    {
        Character character = args.Collider.GetComponentInParent<Character>();

        return (character == triggeringCharacter);
    }
}
