using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigiPen.TextReader
{
    public class TestSyntaxRule : SyntaxRule
    {
        [SerializeField]
        private string replacementString = "Hello World";

        protected override string GetSubstringReplacementFor(string substring)
        {
            return replacementString;
        }
    }
}
