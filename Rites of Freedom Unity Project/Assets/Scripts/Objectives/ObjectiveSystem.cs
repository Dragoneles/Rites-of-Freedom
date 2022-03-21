/******************************************************************************
 * 
 * File: ObjectiveSystem.cs
 * Author: Joseph Crump
 * Date: 3/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Static class used to register player objectives.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Static class used to register player objectives.
/// </summary>
public static class ObjectiveSystem
{
    /// <summary>
    /// Event raised whenever a new objective is added.
    /// </summary>
    public static event Action<Objective> ObjectiveAdded;

    /// <summary>
    /// Event raised whenever an objective is completed.
    /// </summary>
    public static event Action<Objective> ObjectiveCompleted;

    /// <summary>
    /// Event raised whenever an objective is canceled.
    /// </summary>
    public static event Action<Objective> ObjectiveCanceled;

    private static List<Objective> activeObjectives = new List<Objective>();
    private static List<Objective> completedObjectives = new List<Objective>();

    /// <summary>
    /// Add a new objective to the system.
    /// </summary>
    public static void AddObjective(Objective objective)
    {
        if (objective == null)
            return;

        activeObjectives.Add(objective);

        ObjectiveAdded?.Invoke(objective);

        objective.Completed += () => MarkObjectiveComplete(objective);
    }

    /// <summary>
    /// Cancel all objectives.
    /// </summary>
    public static void ClearObjectives()
    {
        activeObjectives.ForEach(o => CancelObjective(o));
    }

    /// <summary>
    /// Cancel an assigned objective.
    /// </summary>
    public static void CancelObjective(Objective objective)
    {
        if (!activeObjectives.Contains(objective))
            return;

        activeObjectives.Remove(objective);

        ObjectiveCanceled?.Invoke(objective);
    }

    private static void MarkObjectiveComplete(Objective objective)
    {
        if (!activeObjectives.Contains(objective))
            return;

        activeObjectives.Remove(objective);

        completedObjectives.Add(objective);

        ObjectiveCompleted?.Invoke(objective);
    }
}
