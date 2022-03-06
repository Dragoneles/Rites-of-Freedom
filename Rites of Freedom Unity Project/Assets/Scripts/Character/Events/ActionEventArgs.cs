/******************************************************************************
 * 
 * File: ActionEventArgs.cs
 * Author: Joseph Crump
 * Date: 2/19/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Event arguments containing information about a Character event.
 *  
 ******************************************************************************/
using System;

/// <summary>
/// Event arguments containing information about a Character event.
/// </summary>
public class ActionEventArgs : EventArgs
{
    /// <summary>
    /// The action triggering the event.
    /// </summary>
    public readonly ActionTypes Action;

    /// <summary>
    /// The state of the triggered event.
    /// </summary>
    public readonly ActionEventType EventType;

    public ActionEventArgs(ActionTypes action, ActionEventType eventType)
    {
        Action = action;
        EventType = eventType;
    }
}
