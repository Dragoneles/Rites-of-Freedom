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
using UnityEngine;
using UnityEngine.Events;
using TMPro;

/// <summary>
/// Objective that can be assigned to / completed by the player.
/// </summary>
public class Objective : MonoBehaviour, IObjective
{
    [Header("Events")]
    [SerializeField] private UnityEvent onCompleted;

    [SerializeField] private UnityEvent onUpdated;

    [Tooltip("Event raised when the objective is started.")]
    [SerializeField] private UnityEvent onStarted;

    /// <summary>
    /// Event raised when the objective is completed.
    /// </summary>
    public event Action Completed;

    /// <summary>
    /// Event raised when the objective is updated.
    /// </summary>
    public event Action Updated;

    [Header("Start conditions")]
    [Tooltip("If true, the objective will start when the scene loads.")]
    [SerializeField] private bool startOnAwake = false;

    [Tooltip("If true, the objective will start when the component is enabled.")]
    [SerializeField] private bool startOnEnable = true;

    [Header("Parameters")]
    [Tooltip("The one-line text prompt shown to the player.")]
    [SerializeField] private string description = "Do the objective: ";
    [Tooltip("Number of times the objective needs to be completed.")]
    [SerializeField] private int repeats = 1;
    [Tooltip("Whether or not to show the progress tracker (x / 5).")]
    [SerializeField] private bool showCounter = true;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI maxProgressText;
    [SerializeField] private GameObject progressTracker;

    // non-serialized fields
    private int timesCompleted = 0;
    private bool objectiveCompleted = false;
    private EventCallback eventCallback;

    private void Awake()
    {
        eventCallback = GetComponent<EventCallback>();

        if (eventCallback == null)
            Debug.LogWarning(
                $"{this} can't update objective. GameObject does not " +
                $"contain a {nameof(EventCallback)} component.");
    }

    private void Start()
    {
        if (!startOnAwake)
            return;

        StartAndAssign();
    }

    private void OnEnable()
    {
        if (!startOnEnable)
            return;

        StartAndAssign();
    }

    private void OnValidate()
    {
        if (descriptionText != null) descriptionText.text = description;
        if (maxProgressText != null) maxProgressText.text = repeats.ToString();
        if (progressTracker != null) progressTracker.SetActive(showCounter);
    }

    /// <summary>
    /// Start the objective and add it to the objective system.
    /// </summary>
    public void StartAndAssign()
    {
        onStarted?.Invoke();
        ObjectiveSystem.AddObjective(this);
        Initialize();
    }

    /// <summary>
    /// Add a callback to be invoked when the objective is completed.
    /// </summary>
    public void AddCompletionCallback(Action action)
    {
        Completed += action;
    }

    private void Initialize()
    {
        eventCallback.Invoked += OnObjectiveUpdated;
    }

    protected virtual void OnObjectiveCompleted()
    {
        objectiveCompleted = true;

        Completed?.Invoke();
        onCompleted?.Invoke();
    }

    protected virtual void OnObjectiveUpdated()
    {
        if (objectiveCompleted)
            return;

        timesCompleted++;

        Updated?.Invoke();
        onUpdated?.Invoke();

        if (timesCompleted >= repeats)
        {
            OnObjectiveCompleted();
        }
    }
}
