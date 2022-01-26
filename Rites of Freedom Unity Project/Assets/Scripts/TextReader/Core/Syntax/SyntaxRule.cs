using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigiPen.TextReader
{
    /// <summary>
    /// Rule for TextFields that changes the way the text is displayed when
    /// parsed at runtime.
    /// </summary>
    public abstract class SyntaxRule
    {
        [SerializeField]
        private string startTerm = "<rule>";

        [SerializeField]
        private string endTerm = "</rule>";

        /// <summary>
        /// Parse a piece of text for syntax substrings and apply the 
        /// syntax rule.
        /// </summary>
        /// <returns>
        /// New string with the syntax rules applied.
        /// </returns>
        public string Parse(string text)
        {
            List<string> syntaxSubstrings = GetSubstringsFrom(text);

            foreach (string syntaxSubstring in syntaxSubstrings)
            {
                text = text.Replace(syntaxSubstring, 
                    GetSubstringReplacementFor(syntaxSubstring));
            }

            return text;
        }

        // Template method, returns a string to replace a substring
        protected abstract string GetSubstringReplacementFor(string substring);

        private List<string> GetSubstringsFrom(string text)
        {
            List<string> substrings = new List<string>();

            if (!TextUsesSyntax(text))
                return substrings;

            List<int> startIndices = new List<int>();
            List<int> endIndices = new List<int>();

            int index = 0;
            while (index > -1)
            {
                int startIndex = text.IndexOf(startTerm, index);

                if (startIndex < 0)
                    break;

                startIndices.Add(startIndex);
                index = startIndex + startTerm.Length;

                int endIndex = text.IndexOf(endTerm, index);

                if (endIndex < 0)
                    break;

                endIndices.Add(endIndex + endTerm.Length);
                index = endIndex;
            }

            for (int i = 0; i < startIndices.Count; i++)
            {
                int startIndex = startIndices[i];
                int endIndex = endIndices[i];
                int length = endIndex - startIndex;

                string substring = text.Substring(startIndex, length);
                substrings.Add(substring);
            }

            return substrings;
        }

        private bool TextUsesSyntax(string text)
        {
            return text.Contains(startTerm) && text.Contains(endTerm);
        }
    }
}
