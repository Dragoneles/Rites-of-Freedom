/******************************************************************************
 * 
 * File: TextCounter.cs
 * Author: Joseph Crump
 * Date: 4/03/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Text handler that displays an integer value.
 *  
 ******************************************************************************/
using UnityEngine;

/// <summary>
/// Text handler that displays an integer value.
/// </summary>
public class TextCounter : TextHandler
{
    [SerializeField] private int value = 0;

    private void OnValidate()
    {
        UpdateDisplay();
    }

    /// <summary>
    /// Increase the value by one.
    /// </summary>
    public void Increment()
    {
        value++;

        UpdateDisplay();
    }

    /// <summary>
    /// Decrease the value by one.
    /// </summary>
    public void Decrement()
    {
        value--;

        UpdateDisplay();
    }

    /// <summary>
    /// Set the value to the specified amount.
    /// </summary>
    public void SetValue(int value)
    {
        this.value = value;

        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        SetText(value.ToString());
    }
}
