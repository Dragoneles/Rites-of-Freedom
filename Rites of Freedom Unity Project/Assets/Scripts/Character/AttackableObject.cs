/******************************************************************************
 * 
 * File: AttackableObject.cs
 * Author: Joseph Crump
 * Date: 3/21/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Behavior that allows an object to raise events when attacked.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Behavior that allows an object to raise events when attacked.
/// </summary>
public class AttackableObject : MonoBehaviour, IAttackable
{
    [SerializeField]
    private UnityEvent attacked = new UnityEvent();

    public void ReceiveAttack(AttackInstance attack)
    {
        attacked?.Invoke();
    }
}
