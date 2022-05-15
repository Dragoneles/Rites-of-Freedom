/******************************************************************************
 * 
 * File: Clipboard.cs
 * Author: Joseph Crump
 * Date: 2/16/22
 * 
 * Copyright © 2022 DigiPen Institute of Technology, all rights reserved.
 * 
 * Summary:
 *  Class used to hold clones of copied data, allowing them to be easily
 *  duplicated.
 *  
 ******************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace AI.BehaviorTree.Editor
{
    /// <summary>
    /// Class used to hold clones of copied data, allowing them to be easily
    /// duplicated.
    /// </summary>
    /// <typeparam name="TObject">
    /// Type inheriting <see cref="UnityEngine.Object"/> in order to use Instantiate to 
    /// clone objects.
    /// </typeparam>
    public class Clipboard<TObject> where TObject : UnityObject
    {
        public bool IsEmpty => objects.Count == 0;

        private List<TObject> objects = new List<TObject>();

        /// <summary>
        /// Write a copy of a list of objects to the clipboard.
        /// </summary>
        public void Write(List<TObject> objectsToCopy)
        {
            Clear();

            objects = CloneObjects(objectsToCopy);
        }

        /// <summary>
        /// Clear the objects stored in the clipboard.
        /// </summary>
        public void Clear()
        {
            foreach (TObject storedObject in objects)
            {
                UnityObject.DestroyImmediate(storedObject);
            }

            objects.Clear();
        }

        /// <summary>
        /// Get a copy of the objects stored in the clipboard.
        /// </summary>
        /// <returns></returns>
        public List<TObject> GetObjects()
        {
            return CloneObjects(objects);
        }

        private List<TObject> CloneObjects(List<TObject> originals)
        {
            List<TObject> clones = new List<TObject>();

            foreach (TObject original in originals)
            {
                TObject clone = UnityObject.Instantiate(original);
                clones.Add(clone);
            }

            PreserveClonedObjectReferences(originals, clones);

            return clones;
        }

        private void PreserveClonedObjectReferences(List<TObject> originals, List<TObject> clones)
        {
            foreach (TObject clonedObject in clones)
            {
                Type objectType = clonedObject.GetType();
                foreach (FieldInfo clonedField in objectType.GetFields())
                {
                    if (clonedField.IsNotSerialized)
                        continue;

                    object clonedFieldValue = clonedField.GetValue(clonedObject);

                    foreach (TObject originalObject in originals)
                    {
                        bool fieldReferencesOriginalObject = (clonedFieldValue == originalObject);

                        if (fieldReferencesOriginalObject)
                        {
                            int indexOfOriginalObject = originals.IndexOf(originalObject);

                            TObject correspondingClone = clones[indexOfOriginalObject];

                            clonedField.SetValue(clonedObject, correspondingClone);
                        }

                        IList fieldAsList = clonedFieldValue as IList;
                        if (fieldAsList == null)
                            continue;

                        if ((fieldAsList).Contains(originalObject))
                        {
                            int indexOfOriginalObject = originals.IndexOf(originalObject);

                            TObject correspondingClone = clones[indexOfOriginalObject];

                            fieldAsList.Remove(originalObject);
                            fieldAsList.Add(correspondingClone);
                        }
                    }
                }
            }
        }
    }
}
