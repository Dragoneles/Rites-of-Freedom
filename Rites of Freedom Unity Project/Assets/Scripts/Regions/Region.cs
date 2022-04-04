/******************************************************************************
 * 
 * File: Region.cs
 * Author: Joseph Crump
 * Date: 4/02/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event dispatcher for a trigger collider.
 *  
 ******************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event dispatcher for a trigger collider.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Region : MonoBehaviour
{
    /// <summary>
    /// Event raised when an object matching the trigger layers enters the region.
    /// </summary>
    public UnityEvent<CollisionEventArgs> ObjectEntered = new();

    /// <summary>
    /// Event raised when an object matching the trigger layers exits the region.
    /// </summary>
    public UnityEvent<CollisionEventArgs> ObjectExited = new();

    private readonly List<GameObject> objectsInRegion = new();
    
    [SerializeField] private LayerMask triggerLayers;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject triggerObject = collision.gameObject;
        int triggerObjectLayer = triggerObject.layer;

        if (!triggerLayers.ContainsLayer(triggerObjectLayer))
            return;

        var args = new CollisionEventArgs(collision);
        ObjectEntered?.Invoke(args);

        objectsInRegion.Add(triggerObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject triggerObject = collision.gameObject;
        int triggerObjectLayer = triggerObject.layer;

        if (!triggerLayers.ContainsLayer(triggerObjectLayer))
            return;

        var args = new CollisionEventArgs(collision);
        ObjectExited?.Invoke(args);

        objectsInRegion.Remove(triggerObject);
    }

    /// <summary>
    /// Get a copy of the list of objects in the region.
    /// </summary>
    public List<GameObject> GetObjects()
    {
        return new List<GameObject>(objectsInRegion);
    }
}
