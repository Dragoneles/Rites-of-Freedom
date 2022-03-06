/******************************************************************************
 * 
 * File: DialogueSpeaker.cs
 * Author: Joseph Crump
 * Date: 2/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that can facilitate conversation objects.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behavior that can facilitate conversation objects.
/// </summary>
public class DialogueSpeaker : MonoBehaviour
{
    [SerializeField]
    private AudioEntry voice;

    [SerializeField]
    private DialogueDisplayer dialogueDisplayer;

    private Coroutine conversationCoroutine { get; set; }
    private AudioManager audioManager { get; set; }

    /// <summary>
    /// Speak each line of dialogue in a conversation, in order.
    /// </summary>
    public void StartConversation(Conversation conversation)
    {
        if (conversationCoroutine != null)
            return;

        conversationCoroutine = StartCoroutine(ConversationLoop(conversation));
    }

    /// <summary>
    /// Speak a line of dialogue.
    /// </summary>
    public void SayLine(Line line)
    {
        PlayVoiceSound();
        dialogueDisplayer?.Display(line);
    }

    private void PlayVoiceSound()
    {
        if (voice == null)
            return;

        if (audioManager == null)
            audioManager = FindObjectOfType<AudioManager>();

        audioManager.PlaySound(voice);
    }

    private IEnumerator ConversationLoop(Conversation conversation)
    {
        int lineIndex = 0;

        while (lineIndex < conversation.LineCount)
        {
            Line nextLine = conversation.Lines[lineIndex];

            SayLine(nextLine);

            yield return new WaitForSeconds(nextLine.Duration);

            lineIndex++;
        }

        SayLine(Line.NoLine());

        conversationCoroutine = null;
    }
}
