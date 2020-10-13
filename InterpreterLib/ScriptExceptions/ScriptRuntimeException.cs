using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.ScriptExceptions
{

    public class ScriptRuntimeException : Exception
    {
        public Token Token { get; private set; }

        public ScriptRuntimeException(Token token, string message) : base($"Runtime error: {message} (token: <{token}>)")
        {
            this.Token = token;
        }

        public ScriptRuntimeException(Token token, string message, Exception innerException) : base($"Runtime error: {message} (token: <{token}>)", innerException)
        {
            this.Token = token;
        }
    }
}
