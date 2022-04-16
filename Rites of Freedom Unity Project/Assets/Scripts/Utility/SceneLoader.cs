/******************************************************************************
 * 
 * File: SceneLoader.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that can load scenes through UnityEvent calls.
 *  
 ******************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Behavior that can load scenes through UnityEvent calls.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Load a scene by build index.
    /// </summary>
    public void LoadScene(int sceneIndex)
    {
        if (SceneManager.GetSceneByBuildIndex(sceneIndex) == null)
        {
            Debug.LogWarning($"Unable to find scene with index {sceneIndex}");
            return;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    /// <summary>
    /// Load a scene by name.
    /// </summary>
    public void LoadScene(string sceneName)
    {
        if (SceneManager.GetSceneByName(sceneName) == null)
        {
            Debug.LogWarning($"Unable to find scene with name {sceneName}");
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    /// <summary>
    /// Load the next scene build index.
    /// </summary>
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;

        LoadScene(nextSceneIndex);
    }

    /// <summary>
    /// Load the previous scene build index.
    /// </summary>
    public void LoadPreviousScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex - 1;

        LoadScene(nextSceneIndex);
    }

    /// <summary>
    /// Reload the current scene.
    /// </summary>
    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        LoadScene(currentSceneIndex);
    }
}
