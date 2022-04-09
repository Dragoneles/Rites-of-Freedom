/*******************************************************************************
* 
* File:      AudioEntry.cs
* Author:    Joseph Crump
* Date:      1/31/2022
* 
* Description:
*     Contains serializable details that allows audio assets to be quickly 
*     created and put to use in the editor.
* 
*******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Contains serializable details that allows audio assets to be quickly created
/// and put to use in the editor.
/// </summary>
[CreateAssetMenu(fileName = "AudioEntry", menuName = "Audio/AudioEntry")]
public class AudioEntry : ScriptableObject, ICollection<AudioEntryVariation>
{
    [Range(0f, 1f)]
    public float BaseVolume = 1f;
    [Range(-3f, 3f)]
    public float BasePitch = 1f;
    [Range(-1f, 1f)]
    public float StereoPan = 0f;

    [Min(0f)]
    [Tooltip("Length of time used to fade out the sound when using the FadeOut operation.")]
    public float FadeOutLength = 1f;

    [Range(-3f, 0f)]
    public float RandomPitchDown = 0f;
    [Range(0f, 3f)]
    public float RandomPitchUp = 0f;

    public bool PlayOnAwake = false;
    public bool Loop = false;
    public AudioMixerGroup Mixer;

    public List<AudioEntryVariation> Variations = new List<AudioEntryVariation>();
    public int Count { get => Variations.Count; }

    public bool IsReadOnly => ((ICollection<AudioEntryVariation>)Variations).IsReadOnly;

    public AudioSourceCollection CreateAudioSources(GameObject parent)
    {
        AudioSourceCollection sourceGroup = new AudioSourceCollection();

        foreach (var variation in Variations)
        {
            var source = variation.CreateAudioSource(parent, this);
            sourceGroup.Add(source);
        }

        return sourceGroup;
    }

    public void Add(AudioEntryVariation item)
    {
        ((ICollection<AudioEntryVariation>)Variations).Add(item);
    }

    public void Clear()
    {
        ((ICollection<AudioEntryVariation>)Variations).Clear();
    }

    public bool Contains(AudioEntryVariation item)
    {
        return ((ICollection<AudioEntryVariation>)Variations).Contains(item);
    }

    public void CopyTo(AudioEntryVariation[] array, int arrayIndex)
    {
        ((ICollection<AudioEntryVariation>)Variations).CopyTo(array, arrayIndex);
    }

    public bool Remove(AudioEntryVariation item)
    {
        return ((ICollection<AudioEntryVariation>)Variations).Remove(item);
    }

    public IEnumerator<AudioEntryVariation> GetEnumerator()
    {
        return ((IEnumerable<AudioEntryVariation>)Variations).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Variations).GetEnumerator();
    }
}
