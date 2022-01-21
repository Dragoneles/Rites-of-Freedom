using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigiPen.TextReader
{
    /// <summary>
    /// Custom field used to map a string field to the project's text 
    /// dictionary.
    /// </summary>
    [System.Serializable]
    public class TextField
    {
        [SerializeField]
        [Tooltip("The address of this text field in the project directory. " +
            "Text fields with the same address will display the same text.")]
        private string key = string.Empty;

        [SerializeField]
        [TextArea]
        private string text = string.Empty;

        /// <summary>
        /// Set the TextField's text.
        /// </summary>
        public void Set(string value)
        {
            text = value;
        }

        public override string ToString()
        {
            return text;
        }

        // Implicit string conversion
        public static implicit operator string(TextField textField)
            => textField.text;
    }
}
