/******************************************************************************/
/**
 * File   - SerializePolymorphicAttribute.cs
 * Author - Joseph Crump
 * Date   - 10/08/2021
 * 
 * Copyright (c) 2021 DigiPen Institute of Technology. All rights Reserved.
 * 
 * Description:
 *   Custom attribute for a serializable field. Fields with this attribute
 *   can be serialized as any polymorphic object of the field's type.
 */
/******************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field)]
public class SerializePolymorphicAttribute : PropertyAttribute
{
    public List<Type> Exceptions = new List<Type>();

    /// <summary>
    /// Attribute constructor
    /// </summary>
    public SerializePolymorphicAttribute() { }

    /// <summary>
    /// Constructor with type restrictions.
    /// </summary>
    public SerializePolymorphicAttribute(params Type[] exceptions)
    {
        Exceptions = exceptions.ToList();
    }
}
