/******************************************************************************
 * 
 * File: FloatUtility.cs
 * Author: Joseph Crump
 * Date: 1/25/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Static extensions for floats, to improve code readability.
 *  
 ******************************************************************************/

/// <summary>
/// Static extensions for floats, to improve code readability.
/// </summary>
public static class FloatUtility
{
    /// <returns>
    /// Returns true if the float is greater than 0f.
    /// </returns>
    public static bool IsPositive(this float value)
    {
        return (value > 0f);
    }

    /// <returns>
    /// Returns true if the float is less than 0f.
    /// </returns>
    public static bool IsNegative(this float value)
    {
        return (value < 0f);
    }
}
