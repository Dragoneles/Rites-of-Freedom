/******************************************************************************
 * 
 * File: Faction.cs
 * Author: Joseph Crump
 * Date: 4/09/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Object used to delineate unit alignments.
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Object used to delineate unit alignments.
/// </summary>
[CreateAssetMenu(fileName = "NewFaction", menuName = "Faction/New Faction")]
public class Faction : ScriptableObject
{
    /// <summary>
    /// The name of the faction (same name as the asset).
    /// </summary>
    /// <remarks>
    /// Note that faction alliances are determined by reference, and not by 
    /// name. Two factions with the same name could be hostile towards each 
    /// other.
    /// </remarks>
    public string Name => name;

    [Tooltip("List of factions that are not considered hostile.")]
    public List<Faction> AlliedFactions = new();
}
