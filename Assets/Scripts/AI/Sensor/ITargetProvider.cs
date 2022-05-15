/******************************************************************************
 * 
 * File: ITargetProvider.cs
 * Author: Joseph Crump
 * Date: 4/11/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Interface that provides a Character target for a behavior tree.
 *  
 ******************************************************************************/

/// <summary>
/// Interface that provides a Character target for a behavior tree.
/// </summary>
public interface ITargetProvider
{
    /// <summary>
    /// Get a Character to serve as a target.
    /// </summary>
    Character GetTarget();
}
