using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Wrapper for a UnityEvent that always includes a sender.
/// </summary>
/// <typeparam name="T">
/// The EventArgs type of the event.
/// </typeparam>
[Serializable]
public class SmartUnityEvent<TEventArgs> : UnityEvent<object, TEventArgs>
    where TEventArgs : EventArgs 
{
    
}
