/*******************************************************************************
* 
* File:      AudioSourceCollection.cs
* Author:    Joseph Crump
* Date:      1/31/2022
* 
* Description:
*     A collection of audio sources that can be used interchangeably. Allows 
*     for sounds with the same tag to have variations.
* 
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// A collection of audio sources that can be used interchangeably. Allows for
/// sounds with the same tag to have variations.
/// </summary>
[System.Serializable]
public class AudioSourceCollection : ICollection<AudioSource>
{
    private List<AudioSource> AudioSources = new List<AudioSource>();
    public int Count { get { return AudioSources.Count; } }

    public bool IsReadOnly => ((ICollection<AudioSource>)AudioSources).IsReadOnly;

    /// <summary>
    /// Indexer to get AudioSources.
    /// </summary>
    public AudioSource this[int i]
    {
        get
        {
            if (i < 0 || i >= AudioSources.Count)
                return null;

            return AudioSources[i];
        }
        set
        {
            if (i < 0 || i >= AudioSources.Count)
                return;

            AudioSources[i] = value;
        }
    }

    /// <summary>
    /// Stop all audio sources in this group that are currently playing.
    /// </summary>
    public void Stop()
    {
        // Stop all sources in the group
        foreach (AudioSource source in AudioSources)
        {
            if (source.isPlaying)
                source.Stop();
        }
    }

    /// <summary>
    /// Fade the volume of the audio sources to 0 and then stop them.
    /// </summary>
    public void FadeOut(float duration)
    {
        // Stop all sources in the group
        foreach (AudioSource source in AudioSources)
        {
            if (!source.isPlaying)
                return;

            var tween = DOTween.To(
                () => source.volume,
                (v) => source.volume = v,
                endValue: 0f,
                duration: duration);

            tween.OnComplete(Stop);
        }
    }

    /// <summary>
    /// Evaluate whether any of the audio sources are playing.
    /// </summary>
    public bool IsPlaying()
    {
        foreach (AudioSource source in AudioSources)
        {
            if (source.isPlaying)
                return true;
        }

        return false;
    }

    public void Add(AudioSource item)
    {
        AudioSources.Add(item);
    }

    public void Remove(AudioSource item)
    {
        AudioSources.Remove(item);
    }

    public void Clear()
    {
        AudioSources.Clear();
    }

    public bool Contains(AudioSource item)
    {
        return AudioSources.Contains(item);
    }

    public void CopyTo(AudioSource[] array, int arrayIndex)
    {
        ((ICollection<AudioSource>)AudioSources).CopyTo(array, arrayIndex);
    }

    bool ICollection<AudioSource>.Remove(AudioSource item)
    {
        return ((ICollection<AudioSource>)AudioSources).Remove(item);
    }

    public IEnumerator<AudioSource> GetEnumerator()
    {
        return ((IEnumerable<AudioSource>)AudioSources).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)AudioSources).GetEnumerator();
    }
}
