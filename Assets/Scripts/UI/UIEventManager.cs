/******************************************************************************
 * 
 * File: UIEventManager.cs
 * Author: Joseph Crump
 * Date: 2/27/22
 * 
 * Copyright ? 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Class that dispatches events to UIElements by tag.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Class that dispatches events to UIElements by tag.
/// </summary>
public class UIEventManager : MonoBehaviour
{
    private UIElementDictionary uiDictionary = new UIElementDictionary();

    private void Awake()
    {
        UIElement.Created += OnUIElementCreated;
        UIElement.Destroyed += OnUIElementDestroyed;
    }

    private void OnDestroy()
    {
        UIElement.Created -= OnUIElementCreated;
        UIElement.Destroyed -= OnUIElementDestroyed;
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
        uiDictionary.SendMessage(id, message, options);
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
        uiDictionary.SendMessage(id, message, parameter, options);
    }

    private void OnUIElementCreated(UIElement element)
    {
        uiDictionary.Add(element);
    }

    private void OnUIElementDestroyed(UIElement element)
    {
        uiDictionary.Remove(element);
    }
}
