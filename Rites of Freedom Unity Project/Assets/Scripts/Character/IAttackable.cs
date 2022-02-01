/******************************************************************************
 * 
 * File: IAttackable.cs
 * Author: Joseph Crump
 * Date: 1/31/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Interface for an object that can be attacked.
 *  
 ******************************************************************************/

/// <summary>
/// Interface for an object that can be attacked.
/// </summary>
public interface IAttackable
{
    void ReceiveAttack(AttackInstance attack);
}
