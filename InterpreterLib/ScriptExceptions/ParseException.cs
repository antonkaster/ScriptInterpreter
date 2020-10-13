using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.ScriptExceptions
{

    public class ParseException : Exception
    {
        public ParseException()
        {
        }

        public ParseException(string message) : base("Parse error: " + message)
        {
        }

        public ParseException(string message, Exception innerException) : base("Parse error: " + message, innerException)
        {
        }
    }
}
