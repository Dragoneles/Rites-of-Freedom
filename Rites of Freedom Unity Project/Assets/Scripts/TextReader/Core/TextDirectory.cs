using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigiPen.TextReader
{
    /// <summary>
    /// Dictionary for <see cref="TextField"/> objects to read and write from.
    /// </summary>
    public class TextDirectory
    {
        private Dictionary<string, string> dictionary = 
            new Dictionary<string, string>();

        /// <summary>
        /// Get a text value from the dictionary by its key/address.
        /// </summary>
        /// <param name="key">
        /// The dictionary key used as the address of the text.
        /// </param>
        public string GetText(string key)
        {
            string text = string.Empty;

            if (!dictionary.ContainsKey(key))
                return text;

            dictionary.TryGetValue(key, out text);

            return text;
        }

        /// <summary>
        /// Set the text value at the specified address.
        /// </summary>
        /// <param name="key">
        /// The address to write the text to.
        /// </param>
        /// <param name="text">
        /// The string value of the text.
        /// </param>
        public void SetText(string key, string text)
        {
            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, text);
                return;
            }

            dictionary[key] = text;
        }

        /// <summary>
        /// Replace a keyed value with a new address.
        /// </summary>
        /// <param name="key">
        /// The existing value of the address.
        /// </param>
        /// <param name="newKey">
        /// The new value of the address.
        /// </param>
        /// <returns>
        /// If the directory doesn't contain the key, returns false.
        /// </returns>
        public bool ChangeKey(string key, string newKey)
        {
            if (!dictionary.ContainsKey(key))
                return false;

            string text;
            dictionary.TryGetValue(key, out text);

            dictionary.Remove(key);

            dictionary.Add(newKey, text);

            return true;
        }
    }
}
