/*******************************************************************************
* 
* File:      AudioFacilitator.cs
* Author:    Joseph Crump
* Date:      1/31/2022
* 
* Description:
*     ScriptableObject that can be accessed by objects in the scene in order to
*     utilize the AudioManager. Allows prefabs to have convenient access to
*     the AudioManager.
* 
*******************************************************************************/
using UnityEngine;

/// <summary>
/// ScriptableObject that can be accessed by objects in the scene in order to
/// utilize the AudioManager.Allows prefabs to have convenient access to
/// the AudioManager.
/// </summary>
[CreateAssetMenu(fileName = "AudioFacilitator", menuName = "Audio/Facilitator")]
public class AudioFacilitator : ScriptableObject
{
    /// <summary>
    /// Reference to audio manager. If this is null, the getter will find it.
    /// </summary>
    private AudioManager _Manager;
    private AudioManager Manager 
    {
        get
        {
            if (_Manager == null)
                FindAudioManager();

            if (_Manager == null)
                CreateNewAudioManager();

            return _Manager;
        }
    }

    private void CreateNewAudioManager()
    {
        _Manager = GameObject.Instantiate(new GameObject()).AddComponent<AudioManager>();
        _Manager.name = nameof(AudioManager);
    }

    private void FindAudioManager()
    {
        _Manager = FindObjectOfType<AudioManager>();
    }

    public void PlaySound(AudioEntry entry)
    {
        Manager.PlaySound(entry);
    }

    public void StopSound(AudioEntry entry)
    {
        Manager.StopSound(entry);
    }

    public void FadeOutSound(AudioEntry entry)
    {
        Manager.FadeOutSound(entry);
    }
}
