/******************************************************************************
 * 
 * File: Objective.cs
 * Author: Joseph Crump
 * Date: 3/20/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Objective that can be assigned to / completed by the player.
 *  
 ******************************************************************************/
using System;

public interface IObjective
{
    event Action Completed;
    event Action Updated;

    void AddCompletionCallback(Action action);
}