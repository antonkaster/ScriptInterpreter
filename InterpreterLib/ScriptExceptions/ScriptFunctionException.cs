using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace InterpreterLib.ScriptExceptions
{
    public class ScriptFunctionException : Exception
    {
        public Token Token { get; private set; }

        public ScriptFunctionException(Token token, string message) : base($"Function <{token}> error: {message}")
        {
            this.Token = token;
        }

        public ScriptFunctionException(Token token, string message, Exception innerException) : base($"Function <{token}> error: {message}", innerException)
        {
            this.Token = token;
        }

    }
}
