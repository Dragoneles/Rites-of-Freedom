/******************************************************************************
 * 
 * File: NotNode.cs
 * Author: Joseph Crump
 * Date: 2/07/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Decorator node that inverts the return value of its child.
 *  
 ******************************************************************************/

using System.Collections;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Decorator node that inverts the return value of its child.
    /// </summary>
    public class NotNode : DecoratorNode
    {
        protected override bool CheckNodeFailed()
        {
            return (Child.State == NodeState.Success);
        }

        protected override bool CheckNodeSucceeded()
        {
            return (Child.State == NodeState.Failure);
        }

        protected override IEnumerator Run()
        {
            yield return Child.Process();
        }
    }
}
