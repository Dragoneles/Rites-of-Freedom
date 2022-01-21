using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigiPen.TextReader
{
    public class SyntaxTester : MonoBehaviour
    {
        [SerializeField]
        private TextField text;

        private void Start()
        {
            TestSyntaxRule testRule = new TestSyntaxRule();

            text.Set(testRule.Parse(text));

            Debug.Log(text);
        }
    }
}
