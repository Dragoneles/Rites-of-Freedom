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
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Behavior that can facilitate conversation objects.
/// </summary>
public class DialogueSpeaker : MonoBehaviour
{
    [SerializeField] 
    private List<ConversationEvent> conversationEvents = new();

    private Dictionary<Conversation, ConversationEvent> conversationEventDictionary = new();

    [SerializeField]
    private AudioEntry voice;

    [SerializeField]
    private DialogueDisplayer dialogueDisplayer;

    [SerializeField]
    private bool repeatConversation = false;

    private bool skipLine = false;
    private Coroutine conversationCoroutine { get; set; }
    private AudioManager audioManager { get; set; }

    private List<Conversation> completedconversations = new();

    private void Awake()
    {
        PopulateEventDictionary();
    }

    /// <summary>
    /// Speak each line of dialogue in a conversation, in order.
    /// </summary>
    public void StartConversation(Conversation conversation)
    {
        if (!repeatConversation && completedconversations.Contains(conversation))
            return;

        if (conversationCoroutine != null)
            return;

        if (conversationEventDictionary.ContainsKey(conversation))
            conversationEventDictionary[conversation].InvokeStartEvent();

        conversationCoroutine = StartCoroutine(ConversationLoop(conversation));

        completedconversations.Add(conversation);
    }

    /// <summary>
    /// Speak a line of dialogue.
    /// </summary>
    public void SayLine(Line line)
    {
        PlayVoiceSound();
        dialogueDisplayer?.Display(line);
    }

    /// <summary>
    /// Set the skip line flag to true.
    /// </summary>
    public void Skip()
    {
        skipLine = true;
    }

    private void PopulateEventDictionary()
    {
        conversationEvents.ForEach(
            o => conversationEventDictionary.Add(o.Conversation, o));
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

            yield return NextLineTriggered(nextLine.Duration);

            lineIndex++;
        }

        dialogueDisplayer.Clear();

        conversationCoroutine = null;

        if (conversationEventDictionary.ContainsKey(conversation))
            conversationEventDictionary[conversation].InvokeFinishEvent();
    }

    private IEnumerator NextLineTriggered(float waitTime)
    {
        float timer = 0f;
        while (timer < waitTime)
        {
            yield return new WaitForEndOfFrame();

            timer += Time.deltaTime;

            if (skipLine)
            {
                skipLine = false;
                break;
            }
        }

        yield break;
    }
}

[System.Serializable]
public class ConversationEvent
{
    [Tooltip("The conversation that the events are bound to.")]
    public Conversation Conversation;

    [Tooltip("Event raised when a conversation starts.")]
    public UnityEvent<EventArgs> ConversationStarted = new();

    [Tooltip("Event raised when a conversation finished.")]
    public UnityEvent<EventArgs> ConversationFinished = new();

    public void InvokeStartEvent()
    {
        ConversationStarted.Invoke(EventArgs.Empty);
    }

    public void InvokeFinishEvent()
    {
        ConversationFinished.Invoke(EventArgs.Empty);
    }
}
