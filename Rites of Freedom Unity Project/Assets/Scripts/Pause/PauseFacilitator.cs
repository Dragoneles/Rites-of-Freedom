/******************************************************************************
 * 
 * File: PauseFacilitator.cs
 * Author: Joseph Crump
 * Date: 4/10/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object that gives gameObjects the ability to pause and unpause the game.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Object that gives gameObjects the ability to pause and unpause the game.
/// </summary>
[CreateAssetMenu(fileName = "PauseFacilitator")]
public class PauseFacilitator : ScriptableObject
{
    /// <summary>
    /// Whether the game is currently paused.
    /// </summary>
    public static bool IsPaused { get; private set; } = false;

    /// <summary>
    /// Number of times the pause operation has been stacked.
    /// </summary>
    private static int pauseStacks = 0;

    /// <summary>
    /// The timeScale of the game at the time it was originally paused.
    /// </summary>
    private static float cachedTimeScale = 1f;

    [SerializeField]
    private UnityEvent gamePaused = new();

    [SerializeField]
    private UnityEvent gameUnpaused = new();

    /// <summary>
    /// Pause the game.
    /// </summary>
    public void Pause()
    {
        SetPaused(true);
    }

    /// <summary>
    /// Unpause the game.
    /// </summary>
    public void Unpause()
    {
        SetPaused(false);
    }

    /// <summary>
    /// Clear all pause stacks and unpause the game.
    /// </summary>
    public void UnpauseAll()
    {
        pauseStacks = 0;
        SetPaused(false);
    }

    /// <summary>
    /// Set the game's current pause state.
    /// </summary>
    public void SetPaused(bool value)
    {
        pauseStacks += value ? 1 : -1;
        pauseStacks = Mathf.Max(0, pauseStacks);

        bool shouldBePaused = pauseStacks > 0;

        if (shouldBePaused == IsPaused)
            return;

        if (!IsPaused)
        {
            // set timescale to 0
            CacheTimeScale();
            Time.timeScale = 0f;

            IsPaused = true;
            gamePaused?.Invoke();
        }
        else if (IsPaused)
        {
            // resume
            Time.timeScale = cachedTimeScale;

            IsPaused = false;
            gameUnpaused?.Invoke();
        }
    }

    private static void CacheTimeScale()
    {
        cachedTimeScale = Time.timeScale;
    }
}
