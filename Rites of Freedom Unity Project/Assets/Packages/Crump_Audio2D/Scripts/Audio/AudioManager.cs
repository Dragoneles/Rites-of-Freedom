﻿/*******************************************************************************
* File:      AudioManager.cs
* Author:    Joseph Crump
* Date:      1/31/2022
* 
* Description:
*     Singleton object used to play audio clips from anywhere in the project.
* 
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Singleton object used to play audio clips from anywhere in the project.
/// </summary>
public class AudioManager : MonoBehaviour
{
    public List<AudioEntry> AudioEntries = new List<AudioEntry>();
    private Dictionary<AudioEntry, AudioSourceCollection> AudioSourceDictionary =
        new Dictionary<AudioEntry, AudioSourceCollection>();

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        EnforceSingleton();
        Initialize();
    }

    private void EnforceSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void Initialize()
    {
        // Initialize the AudioSources Dictionary
        AudioSourceDictionary = new Dictionary<AudioEntry, AudioSourceCollection>();

        foreach (var entry in AudioEntries)
        {
            AddEntry(entry);
        }
    }

    private void AddEntry(AudioEntry entry)
    {
        var key = entry;
        var sourceGroup = entry.CreateAudioSources(gameObject);

        AudioSourceDictionary.Add(key, sourceGroup);

        if (entry.PlayOnAwake)
        {
            PlaySound(entry);
        }
    }

    public void PlaySound(AudioEntry entry, int index)
    {
        if (entry == null)
            return;

        AudioSourceCollection sourceGroup = GetOrAddAudioSourceGroup(entry);

        AudioSource source = sourceGroup[index];

        if (source == null)
        {
            Debug.LogError($"Unable to get audio source at index {index}.");
            return;
        }

        // Calculate pitch variance
        float pitchDown = entry.RandomPitchDown;
        float pitchUp = entry.RandomPitchUp;
        float pitchVariance = UnityEngine.Random.Range(pitchDown, pitchUp);

        // Modify source pitch
        source.pitch = entry.BasePitch + pitchVariance;

        source.Play();
    }

    /// <summary>
    /// Play a random variation of an audio entry.
    /// </summary>
    public void PlaySound(AudioEntry entry)
    {
        int random = UnityEngine.Random.Range(0, entry.Count);
        PlaySound(entry, random);
    }

    public void StopSound(AudioEntry entry)
    {
        if (entry == null)
            return;

        AudioSourceCollection audioSources = GetOrAddAudioSourceGroup(entry);
        audioSources.Stop();
    }

    private AudioSourceCollection GetOrAddAudioSourceGroup(AudioEntry entry)
    {
        if (!AudioSourceDictionary.ContainsKey(entry))
            AddEntry(entry);

        AudioSourceCollection sourceGroup;
        AudioSourceDictionary.TryGetValue(entry, out sourceGroup);

        return sourceGroup;
    }
}