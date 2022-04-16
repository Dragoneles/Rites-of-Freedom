/******************************************************************************
 * 
 * File: CheatDispatcher.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object that records inputs and uses them to dispatch cheats.
 *  
 ******************************************************************************/
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Object that records inputs and uses them to dispatch cheats.
/// </summary>
public class HotkeyTrigger : MonoBehaviour
{
    [SerializeField]
    private List<HotkeyEvent> events = new List<HotkeyEvent>();

    private Dictionary<Key, UnityEvent> eventDictionary = new Dictionary<Key, UnityEvent>();

    private void Awake()
    {
        PopulateDictionary();

        OnInitialize();
    }

    private void PopulateDictionary()
    {
        foreach (HotkeyEvent cheat in events)
        {
            eventDictionary.Add(cheat.Key, cheat.Action);
        }
    }

    private void Update()
    {
        if (ReadKeys(out Key key))
        {
            eventDictionary[key].Invoke();
        }
    }

    protected virtual void OnInitialize() { }

    private bool ReadKeys(out Key keyPressed)
    {
        foreach (Key key in eventDictionary.Keys)
        {
            if (Keyboard.current[key].wasPressedThisFrame)
            {
                keyPressed = key;
                return true;
            }    
        }

        keyPressed = Key.None;
        return false;
    }
}

[System.Serializable]
public class HotkeyEvent
{
    public Key Key = Key.F1;
    public UnityEvent Action = new UnityEvent();
}
