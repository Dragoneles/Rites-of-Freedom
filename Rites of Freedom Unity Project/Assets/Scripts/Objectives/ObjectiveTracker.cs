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
    internal class ObjectiveDisplayDictionary : Dictionary<Objective, ObjectiveDisplay> { }

    [SerializeField]
    private GameObject objectiveDisplayPrefab;

    private ObjectiveDisplayDictionary objectiveDisplays = new ObjectiveDisplayDictionary();

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        ObjectiveSystem.ObjectiveAdded += OnObjectiveAdded;
        ObjectiveSystem.ObjectiveCompleted += OnObjectiveCompleted;
        ObjectiveSystem.ObjectiveCanceled += OnObjectiveCanceled;
    }

    protected void OnObjectiveAdded(Objective objective)
    {
        ObjectiveDisplay display = CreateObjectiveDisplay();

        if (display == null)
            return;

        display.TrackObjective(objective);

        objectiveDisplays.Add(objective, display);
    }

    protected void OnObjectiveCompleted(Objective objective)
    {
        if (!objectiveDisplays.ContainsKey(objective))
            return;

        objectiveDisplays[objective].CompleteAndDestroy();

        objectiveDisplays.Remove(objective);
    }

    protected void OnObjectiveCanceled(Objective objective)
    {
        if (!objectiveDisplays.ContainsKey(objective))
            return;

        objectiveDisplays[objective].CancelAndDestroy();

        objectiveDisplays.Remove(objective);
    }

    private ObjectiveDisplay CreateObjectiveDisplay()
    {
        var obj = Instantiate(objectiveDisplayPrefab, transform);
        var display = obj.GetComponentInChildren<ObjectiveDisplay>();

        if (display == null)
        {
            Debug.LogWarning(
                $"Prefab {objectiveDisplayPrefab} does not have" +
                $" a {nameof(ObjectiveDisplay)} component.");

            return null;
        }

        return display;
    }
}
