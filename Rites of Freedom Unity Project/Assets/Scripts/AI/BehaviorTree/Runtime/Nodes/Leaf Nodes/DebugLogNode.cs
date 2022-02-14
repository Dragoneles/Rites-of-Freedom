/******************************************************************************
 * 
 * File: DebugLogNode.cs
 * Author: Joseph Crump
 * Date: 2/12/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior tree node that logs a debug message.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Behavior tree node that logs a debug message.
    /// </summary>
    public class DebugLogNode : LeafNode
    {
        protected enum LogType { Message, Warning, Error }

        [SerializeField]
        protected LogType logType = LogType.Message;

        [SerializeField]
        protected string message = string.Empty;

        protected override void Start()
        {
            switch (logType)
            {
                case LogType.Message:
                    Debug.Log(message);
                    break;

                case LogType.Warning:
                    Debug.LogWarning(message);
                    break;

                case LogType.Error:
                    Debug.LogError(message);
                    break;

                default:
                    break;
            }
        }

        protected override bool NodeFailed() => false;
        protected override bool NodeSucceeded() => true;
    }
}
