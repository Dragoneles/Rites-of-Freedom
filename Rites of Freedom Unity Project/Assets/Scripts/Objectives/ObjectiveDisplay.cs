/******************************************************************************
 * 
 * File: ObjectiveDisplay.cs
 * Author: Joseph Crump
 * Date: 3/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Component that handles the displaying of objective progress.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Component that handles the displaying of objective progress.
/// </summary>
public class ObjectiveDisplay : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDisplay;

    /// <summary>
    /// Start tracking an objective.
    /// </summary>
    public void TrackObjective(Objective objective)
    {
        SetTextDisplay(objective.GetProgressText());

        objective.Updated += () => OnObjectiveUpdated(objective);
    }

    /// <summary>
    /// Notify the display that the objective is complete, then destroy it.
    /// </summary>
    public void CompleteAndDestroy()
    {
        // TO-DO: implement sequence
        Destroy(gameObject);
    }

    /// <summary>
    /// Notify the display that the objective is canceled, then destroy it.
    /// </summary>
    public void CancelAndDestroy()
    {
        // TO-DO: implement sequence
        Destroy(gameObject);
    }

    private void OnObjectiveUpdated(Objective objective)
    {
        SetTextDisplay(objective.GetProgressText());
    }

    private void SetTextDisplay(string text)
    {
        textDisplay.text = text;
    }
}
