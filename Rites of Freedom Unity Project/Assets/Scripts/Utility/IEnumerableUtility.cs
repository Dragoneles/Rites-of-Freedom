/******************************************************************************
 * 
 * File: IEnumerableUtility.cs
 * Author: Joseph Crump
 * Date: 2/18/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Extension methods for IEnumerable collections
 *  
 ******************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Extension methods for IEnumerable collections
/// </summary>
public static class IEnumerableUtility
{
    /// <summary>
    /// Get the most frequently occurring elements in a collection.
    /// </summary>
    /// <returns>
    /// Returns all values that share the mode occurrences.
    /// If the enumerable is empty, returns an empty list.
    /// </returns>
    public static List<T> Mode<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable.Count() == 0)
            return new List<T>();

        Dictionary<T, int> elementCountDictionary = new Dictionary<T, int>();

        foreach (var element in enumerable)
        {
            if (!elementCountDictionary.ContainsKey(element))
                elementCountDictionary.Add(element, 0);

            elementCountDictionary[element]++;
        }

        var orderedEnumerable = elementCountDictionary.OrderBy((o) => o.Value);

        int modeCount = orderedEnumerable.First().Value;
        var modes = new List<T> { orderedEnumerable.First().Key };

        // Find all other objects with the same occurrence rate
        foreach (var element in orderedEnumerable)
        {
            if (element.Value < modeCount)
                break;

            if (modes.Contains(element.Key))
                continue;

            modes.Add(element.Key);
        }

        return modes;
    }
}
