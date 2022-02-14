/******************************************************************************
 * 
 * File: MoveNode.cs
 * Author: Joseph Crump
 * Date: 2/12/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior tree node that tells the agent to move.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Behavior tree node that tells the agent to move.
    /// </summary>
    public class MoveNode : LeafNode
    {
        public enum Direction { Left, Right }

        [SerializeField]
        protected Direction direction;

        [SerializeField]
        protected float duration = 1f;

        protected override IEnumerator Run()
        {
            throw new System.NotImplementedException();
        }

        protected override bool NodeFailed()
        {
            throw new System.NotImplementedException();
        }

        protected override bool NodeSucceeded()
        {
            throw new System.NotImplementedException();
        }
    }
}
