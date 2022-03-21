/******************************************************************************
 * 
 * File: Objective.cs
 * Author: Joseph Crump
 * Date: 3/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Objective that can be assigned to / completed by the player.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Objective that can be assigned to / completed by the player.
/// </summary>
public class Objective
{
    /// <summary>
    /// Event raised when the objective is completed.
    /// </summary>
    public event Action Completed;

    /// <summary>
    /// Event raised when the objective is updated.
    /// </summary>
    public event Action Updated;

    protected string displayTextPrefix = string.Empty;

    public Objective()
    {
        Initialize();
    }

    public Objective(string displayTextPrefix) : this()
    {
        this.displayTextPrefix = displayTextPrefix;
    }

    /// <summary>
    /// Get the text indicating the objective's progress.
    /// </summary>
    public virtual string GetProgressText()
    {
        return displayTextPrefix;
    }

    /// <summary>
    /// Add a callback to be invoked when the objective is completed.
    /// </summary>
    public void AddCompletionCallback(Action action)
    {
        Completed += action;
    }

    protected virtual void OnObjectiveCompleted()
    {
        Completed?.Invoke();
    }

    protected virtual void OnObjectiveUpdated()
    {
        Updated?.Invoke();
    }

    protected virtual void Initialize() { }
}
