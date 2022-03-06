/******************************************************************************
 * 
 * File: IListUtility.cs
 * Author: Joseph Crump
 * Date: 2/18/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension methods for IList collections
 *  
 ******************************************************************************/
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Extension methods for IList collections
/// </summary>
public static class IListUtility
{
    /// <summary>
    /// Get a random element from a collection.
    /// </summary>
    public static T GetRandom<T>(this IList<T> list)
    {
        int index = Random.Range(0, list.Count);
        return list[index];
    }

    /// <summary>
    /// Compare an index in a list with another list.
    /// </summary>
    /// <param name="index">
    /// The index used to start the comparison.
    /// </param>
    /// <param name="sequence">
    /// The sequence to compare the list to.
    /// </param>
    /// <returns>
    /// Returns true if all elements starting at the specified 
    /// index match the compared sequence.
    /// </returns>
    public static bool IndexMatchesSequence<T>(this IList<T> list, int index, IList<T> sequence)
    {
        foreach (var element in sequence)
        {
            if (index >= list.Count)
                return false;

            if (!list[index].Equals(element))
                return false;

            index++;
        }

        return true;
    }
}
