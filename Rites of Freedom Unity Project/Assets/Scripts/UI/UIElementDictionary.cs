/******************************************************************************
 * 
 * File: UIElementDictionary.cs
 * Author: Joseph Crump
 * Date: 2/27/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  First class dictionary of UIElement ID and group pairings.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// First class dictionary of UIElement ID and group pairings.
/// </summary>
public class UIElementDictionary
{
    private Dictionary<string, UIElementGroup> elementDictionary;

    public UIElementDictionary()
    {
        elementDictionary = new Dictionary<string, UIElementGroup>();
    }

    /// <summary>
    /// Find al UIElements in the scene and add them to the dictionary, keyed
    /// by any of their IDs.
    /// </summary>
    public void Populate()
    {
        if (elementDictionary == null)
            elementDictionary = new Dictionary<string, UIElementGroup>();

        UIElement[] elements = Object.FindObjectsOfType<UIElement>(includeInactive: true);

        foreach (UIElement element in elements)
        {
            AddElementIDs(element);
        }
    }

    /// <summary>
    /// Broadcast a message to every UIElement with the given ID.
    /// </summary>
    /// <param name="message">
    /// Name of the method that should be invoked.
    /// </param>
    public void BroadcastMessage(string id, string message)
    {
        BroadcastMessage(id, message, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Broadcast a message to every UIElement with the given ID.
    /// </summary>
    /// <param name="message">
    /// Name of the method that should be invoked.
    /// </param>
    /// <param name="parameter">
    /// Optional parameter to include when sending the message.
    /// </param>
    public void BroadcastMessage(string id, string message, object parameter)
    {
        BroadcastMessage(id, message, parameter, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Broadcast a message to every UIElement with the given ID.
    /// </summary>
    /// <param name="message">
    /// Name of the method that should be invoked.
    /// </param>
    /// <param name="options">
    /// Whether or not to throw an error if the UIElement does not have the 
    /// requested provided method signature.
    /// </param>
    public void BroadcastMessage(string id, string message, SendMessageOptions options)
    {
        if (!elementDictionary.ContainsKey(id))
            return;

        elementDictionary[id].RemoveNullReferences();
        elementDictionary[id].ForEach(
            (o) =>
            {
                o.BroadcastMessage(message, options);
            });
    }

    /// <summary>
    /// Broadcast a message to every UIElement with the given ID.
    /// </summary>
    /// <param name="message">
    /// Name of the method that should be invoked.
    /// </param>
    /// <param name="parameter">
    /// Optional parameter to include when sending the message.
    /// </param>
    /// <param name="options">
    /// Whether or not to throw an error if the UIElement does not have the 
    /// requested provided method signature.
    /// </param>
    public void BroadcastMessage(string id, string message, object parameter, SendMessageOptions options)
    {
        if (!elementDictionary.ContainsKey(id))
            return;

        elementDictionary[id].RemoveNullReferences();
        elementDictionary[id].ForEach(
            (o) => 
            {
                o.BroadcastMessage(message, parameter, options);
            });
    }

    private void AddElementIDs(UIElement element)
    {
        foreach (string id in element.IDs)
        {
            TryAddKey(id);
            TryAddElement(element, id);
        }
    }

    private void TryAddElement(UIElement element, string id)
    {
        if (!elementDictionary[id].Contains(element))
            elementDictionary[id].Add(element);
    }

    private void TryAddKey(string id)
    {
        if (!elementDictionary.ContainsKey(id))
            elementDictionary.Add(id, new UIElementGroup());
    }
}
