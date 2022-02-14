/******************************************************************************
 * 
 * File: NodeState.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Enum types for all possible node states.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Enum types for all possible node states.
    /// </summary>
    public enum NodeState
    {
        Inactive = 0,
        Running = 1,
        Success = 2,
        Failure = 3
    }
}
