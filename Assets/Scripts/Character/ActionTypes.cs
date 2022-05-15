/******************************************************************************
 * 
 * File: ActionTypes.cs
 * Author: Joseph Crump
 * Date: 2/27/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Flag enumerator for different action types.
 *  
 ******************************************************************************/

/// <summary>
/// Flag enumerator for different action types.
/// </summary>
[System.Flags]
public enum ActionTypes
{
    None = 0,
    Move = 1,
    Jump = 2,
    Roll = 4,
    Attack = 8,
    Block = 16,

    Story = Move + Jump + Roll,
    Combat = Story + Attack + Block
}
