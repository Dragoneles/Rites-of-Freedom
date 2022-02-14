/******************************************************************************
 * 
 * File: ICloneable.cs
 * Author: Joseph Crump
 * Date: 2/11/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Interface establishing that an object can be cloned.
 *  
 ******************************************************************************/

namespace AI.BehaviorTree
{
    /// <summary>
    /// Interface establishing that an object can be cloned.
    /// </summary>
    public interface ICloneable<T>
    {
        T Clone();
    }
}