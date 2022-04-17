/******************************************************************************
 * 
 * File: DeviceChangeListener.cs
 * Author: Joseph Crump
 * Date: 4/16/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Singleton object that raises an event when an input device is paired.
 *  
 ******************************************************************************/
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.EventSystems;

/// <summary>
/// Singleton object that raises an event when an input device is paired.
/// </summary>
public class DeviceChangeListener : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the object.
    /// </summary>
    public static DeviceChangeListener Instance;

    /// <summary>
    /// Event raised whenever a new input device is paired.
    /// </summary>
    public event Action<InputDevice> DevicePaired;

    /// <summary>
    /// Event raised whenever an input device is unpaired.
    /// </summary>
    public event Action<InputDevice> DeviceUnpaired;

    /// <summary>
    /// Event raised when a gamepad is paired.
    /// </summary>
    public event Action GamepadPaired;

    /// <summary>
    /// Event raised when a gamepad is unpaired.
    /// </summary>
    public event Action GamepadUnpaired;

    /// <summary>
    /// The current active device.
    /// </summary>
    public InputDevice CurrentDevice { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InputUser.onChange += OnInputUserChange;
    }

    private void OnEnable()
    {
        InputUser.onChange += OnInputUserChange;
    }

    private void OnDisable()
    {
        InputUser.onChange -= OnInputUserChange;
    }

    private void OnDestroy()
    {
        InputUser.onChange -= OnInputUserChange;
    }

    private void OnInputUserChange(InputUser user, InputUserChange change, InputDevice device)
    {
        if (change != InputUserChange.DevicePaired)
            return;

        OnDeviceUnpaired(CurrentDevice);

        if (CurrentDevice is Gamepad)
            OnGamepadUnpaired();

        CurrentDevice = device;

        OnDevicePaired(CurrentDevice);

        if (CurrentDevice is Gamepad)
            OnGamepadPaired();
    }

    protected virtual void OnDevicePaired(InputDevice device)
    {
        DevicePaired?.Invoke(device);
    }

    protected virtual void OnDeviceUnpaired(InputDevice device)
    {
        if (CurrentDevice == null)
            return;

        DeviceUnpaired?.Invoke(device);
    }

    protected virtual void OnGamepadPaired()
    {
        GamepadPaired?.Invoke();

        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);

        Cursor.visible = false;
    }

    protected virtual void OnGamepadUnpaired()
    {
        GamepadUnpaired?.Invoke();

        Cursor.visible = true;
    }
}
