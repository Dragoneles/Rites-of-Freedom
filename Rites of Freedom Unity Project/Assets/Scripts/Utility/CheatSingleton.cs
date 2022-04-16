/******************************************************************************
 * 
 * File: CheatSingleton.cs
 * Author: Joseph Crump
 * Date: 4/15/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Singleton object that processes the game's cheat commands.
 *  
 ******************************************************************************/

/// <summary>
/// Singleton object that processes the game's cheat commands.
/// </summary>
public class CheatSingleton : HotkeyTrigger
{
    /// <summary>
    /// Singleton instance of the object.
    /// </summary>
    public static CheatSingleton Instance;

    protected override void OnInitialize()
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
}
