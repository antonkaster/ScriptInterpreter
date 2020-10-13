using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace InterpreterLib.ScriptExceptions
{
    public class TokenizeException : Exception
    {
        public TokenizeException()
        {
        }

        public TokenizeException(string message) : base("Tokenize error: " + message)
        {
        }

        public TokenizeException(string message, Exception innerException) : base("Tokenize error: " + message, innerException)
        {
        }
    }
}
