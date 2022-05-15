/******************************************************************************
 * 
 * File: ObjectiveTracker.cs
 * Author: Joseph Crump
 * Date: 3/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object that tracks objective progress.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object that tracks objective progress.
/// </summary>
public class ObjectiveTracker : MonoBehaviour
{
    private class ObjectiveDisplayDictionary : Dictionary<IObjective, ObjectiveDisplay> { }

    [SerializeField]
    private GameObject objectiveDisplayPrefab;

    private ObjectiveDisplayDictionary objectiveDisplays = new();

    private void Awake()
    {
        Initialize();
    }

    private void OnDestroy()
    {
        ObjectiveSystem.ObjectiveAdded -= OnObjectiveAdded;
        ObjectiveSystem.ObjectiveCompleted -= OnObjectiveCompleted;
        ObjectiveSystem.ObjectiveCanceled -= OnObjectiveCanceled;
    }

    private void Initialize()
    {
        ObjectiveSystem.ObjectiveAdded += OnObjectiveAdded;
        ObjectiveSystem.ObjectiveCompleted += OnObjectiveCompleted;
        ObjectiveSystem.ObjectiveCanceled += OnObjectiveCanceled;
    }

    protected void OnObjectiveAdded(IObjective objective)
    {

    }

    protected void OnObjectiveCompleted(IObjective objective)
    {
        if (!objectiveDisplays.ContainsKey(objective))
            return;

        objectiveDisplays[objective].CompleteAndDestroy();

        objectiveDisplays.Remove(objective);
    }

    protected void OnObjectiveCanceled(IObjective objective)
    {
        if (!objectiveDisplays.ContainsKey(objective))
            return;

        objectiveDisplays[objective].CancelAndDestroy();

        objectiveDisplays.Remove(objective);
    }
}
