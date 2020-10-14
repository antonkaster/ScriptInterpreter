using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpreterLib.LexerModules
{
    internal static class LexerHelper
    {
        private static readonly string[] startOfExpr;
        private static readonly string[] endOfExpr;
        private static readonly string[] tokenSeparators;
        private static readonly string identifierSymbols = "qwertyuiopasdfghjklzxcvbnm1234567890_";
        private static readonly string digitSymbols = "1234567890.,";

        static LexerHelper()
        {
            startOfExpr = new string[]{ "(", "[", "{" };
            endOfExpr = new string[]{ ")", "]", "}" };
            tokenSeparators = new string[] { ",", ";", " ", "\r", "\n", "\"", "\t" }
                .Concat(startOfExpr)
                .Concat(endOfExpr)
                .ToArray();
        }

        public static bool IsNumeric(char c)
        {
            return digitSymbols.IndexOf(c) > -1;
        }

        public static bool IsNumeric(string s)
        {
            if (s.Length == 0)
                return false;

            foreach (char c in s)
            {
                if (!IsNumeric(c))
                    return false;
            }
            return true;
        }
        public static string[] GetTokenSepartors()
        {
            return tokenSeparators.ToArray();
        }

        public static bool IsCorrectIdentifier(char c) => IsCorrectIdentifier(c.ToString());
        public static bool IsCorrectIdentifier(string text)
        {
            if (text == null)
                return false;

            text = text.ToLower();

            foreach (char s in text)
            {
                if (!identifierSymbols.Contains(s))
                    return false;
            }
            return true;
        }

        public static bool IsTokenSeparator(char c) => IsTokenSeparator(c.ToString());
        public static bool IsTokenSeparator(string text) => tokenSeparators.Contains(text?.ToLower());

        public static bool IsStartOfExpression(string text) => startOfExpr.Contains(text?.ToLower());
        public static bool IsEndOfExpression(string text) => endOfExpr.Contains(text?.ToLower());


    }
}
