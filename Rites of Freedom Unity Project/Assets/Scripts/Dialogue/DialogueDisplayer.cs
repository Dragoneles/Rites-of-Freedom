/******************************************************************************
 * 
 * File: DialogueDisplayer.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior used to display written lines of dialogue.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior used to display written lines of dialogue.
/// </summary>
[RequireComponent(typeof(CanvasGroupFader), typeof(TextHandler))]
public class DialogueDisplayer : MonoBehaviour
{
    [SerializeField]
    private TextHandler textHandler;

    [SerializeField]
    private CanvasGroupFader fader;

    private Coroutine displayCoroutine;

    private void OnValidate()
    {
        fader = GetComponent<CanvasGroupFader>();
        textHandler = GetComponent<TextHandler>();
    }

    /// <summary>
    /// Write a dialogue line to the display.
    /// </summary>
    public void Display(Line line)
    {
        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);

        fader.SetAlpha(0f);
        displayCoroutine = StartCoroutine(DisplayCoroutine(line.Text, line.Duration));
    }

    /// <summary>
    /// Clear all text from the text mesh.
    /// </summary>
    public void Clear()
    {
        if (displayCoroutine != null)
            StopCoroutine(displayCoroutine);

        displayCoroutine = StartCoroutine(DisplayCoroutine(string.Empty, 0f));
    }

    private IEnumerator DisplayCoroutine(string text, float duration)
    {
        float displayLength = duration - fader.GetFadeTime();
        displayLength = Mathf.Max(0f, displayLength);

        textHandler.SetText(text);

        fader.FadeCanvas(1f);

        yield return new WaitForSeconds(displayLength);

        fader.FadeCanvas(0f);

        yield return new WaitForSeconds(fader.GetFadeTime());

        displayCoroutine = null;
    }
}
