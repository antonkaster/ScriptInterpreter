using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace InterpreterLib.ScriptExceptions
{
    public class ScriptControlException : Exception
    {

        public ScriptControlException(string message) : base("Script control error: " + message)
        {
        }

        public ScriptControlException(string message, Exception innerException) : base("Script control error: " +  message, innerException)
        {
        }

    }
}
