/******************************************************************************
 * 
 * File: AttackInfo.cs
 * Author: Joseph Crump
 * Date: 2/05/22
 * 
 * Copyright � 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Class containing information pertaining to an attack.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class containing information pertaining to an attack.
/// </summary>
[System.Serializable]
public class AttackInfo
{
    public Stat Damage = new Stat(3);
    public Stat StaminaCost = new Stat(5);
}