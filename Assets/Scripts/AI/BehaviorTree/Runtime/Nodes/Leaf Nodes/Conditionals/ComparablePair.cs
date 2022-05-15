/******************************************************************************
 * 
 * File: ComparisonPair.cs
 * Author: Joseph Crump
 * Date: 2/13/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Pair of objects that can be compared.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI.BehaviorTree
{
    /// <summary>
    /// Pair of objects that can be compared.
    /// </summary>
    public abstract class ComparablePair 
    {
        public abstract IComparable ValueA { get; }
        public abstract IComparable ValueB { get; }

        /// <summary>
        /// Compare the two values and return the result.
        /// </summary>
        /// <param name="comparisonType">
        /// How to compare the values.
        /// </param>
        public bool Evaluate(ComparisonType comparisonType)
        {
            switch (comparisonType)
            {
                case ComparisonType.Equal: 
                    return ValueA.CompareTo(ValueB) == 0;

                case ComparisonType.NotEqual: 
                    return ValueA.CompareTo(ValueB) != 0;

                case ComparisonType.GreaterThan: 
                    return ValueA.CompareTo(ValueB) > 0;

                case ComparisonType.GreaterThanOrEqual:
                    return ValueA.CompareTo(ValueB) >= 0;

                case ComparisonType.LessThan:
                    return ValueA.CompareTo(ValueB) < 0;

                case ComparisonType.LessThanOrEqual:
                    return ValueA.CompareTo(ValueB) <= 0;

                default:
                    break;
            }

            return false;
        }
    }

    /// <summary>
    /// Pair of objects that can be compared.
    /// </summary>
    [Serializable]
    public class ComparablePair<T> : ComparablePair where T : IComparable
    {
        [SerializeField]
        private T A;

        [SerializeField]
        private T B;

        public ComparablePair() { }
        public ComparablePair(T a, T b)
        {
            A = a;
            B = b;
        }

        public override IComparable ValueA => A;

        public override IComparable ValueB => B;
    }

    [Serializable] public class IntPair : ComparablePair<int> { }
    [Serializable] public class FloatPair : ComparablePair<float> { }
}
