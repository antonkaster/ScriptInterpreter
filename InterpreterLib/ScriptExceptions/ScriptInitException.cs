using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace InterpreterLib.ScriptExceptions
{
    public class ScriptInitException : Exception
    {
        public ScriptInitException()
        {
        }

        public ScriptInitException(string message) : base($"Language init error: " + message)
        {
        }

        public ScriptInitException(string message, Exception innerException) : base($"Language init error: " + message, innerException)
        {
        }

    }
}
