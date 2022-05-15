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
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// First class dictionary of UIElement ID and group pairings.
/// </summary>
public class UIElementDictionary
{
    private Dictionary<string, UIElement> elementDictionary = new Dictionary<string, UIElement>();

    /// <summary>
    /// Add a new UI element to the dictionary.
    /// </summary>
    public void Add(UIElement element)
    {
        string id = element.ID;
        if (elementDictionary.ContainsKey(id))
        {
            Debug.LogWarning(
                $"Unable to add {element.gameObject} to the " +
                $"global UI dictionary. An element with the " +
                $"ID \'{id}\' already exists.");
            return;
        }

        elementDictionary.Add(id, element);
    }

    /// <summary>
    /// Remove a UI element from the dictionary.
    /// </summary>
    public void Remove(UIElement element)
    {
        string id = element.ID;

        if (!elementDictionary.ContainsKey(id))
            return;

        elementDictionary.Remove(id);
    }

    /// <summary>
    /// Clear all elements from the dictionary.
    /// </summary>
    public void Reset()
    {
        elementDictionary.Clear();
    }

    /// <summary>
    /// Send a message to the UIElement with the given ID.
    /// </summary>
    /// <param name="message">
    /// Name of the method that should be invoked.
    /// </param>
    public void SendMessage(string id, string message)
    {
        SendMessage(id, message, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Send a message to the UIElement with the given ID.
    /// </summary>
    /// <param name="message">
    /// Name of the method that should be invoked.
    /// </param>
    /// <param name="parameter">
    /// Optional parameter to include when sending the message.
    /// </param>
    public void SendMessage(string id, string message, object parameter)
    {
        SendMessage(id, message, parameter, SendMessageOptions.DontRequireReceiver);
    }

    /// <summary>
    /// Send a message to the UIElement with the given ID.
    /// </summary>
    /// <param name="message">
    /// Name of the method that should be invoked.
    /// </param>
    /// <param name="options">
    /// Whether or not to throw an error if the UIElement does not have the 
    /// requested provided method signature.
    /// </param>
    public void SendMessage(string id, string message, SendMessageOptions options)
    {
        if (!elementDictionary.ContainsKey(id))
            return;

        elementDictionary[id].BroadcastMessage(message, options);
    }

    /// <summary>
    /// Send a message to the UIElement with the given ID.
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
    public void SendMessage(string id, string message, object parameter, SendMessageOptions options)
    {
        if (!elementDictionary.ContainsKey(id))
            return;

        elementDictionary[id].BroadcastMessage(message, parameter, options);
    }
}
