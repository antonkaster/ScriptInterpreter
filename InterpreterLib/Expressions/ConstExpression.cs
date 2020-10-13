using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Logs;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Runtime.CompilerServices;

namespace InterpreterLib.Expressions
{
    public class ConstExpression : Expression
    {
        private SObject constValue = null;

        public ConstExpression(IInternalRuntimeControl internalControl, Token token) 
            : base(internalControl, token)
        {
            switch (Token.TokenType)
            {
                case TokenType.Numeric:
                    string tokenString = Token.TokenString.Replace(',', '.');
                    decimal num;
                    if (!decimal.TryParse(tokenString, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                        throw new ArgumentException($"Wrong numeric value '{Token.TokenString}'");
                    constValue = new SObject() { NumValue = num };
                    break;
                case TokenType.Text:
                    constValue = new SObject() { StringValue = Token.TokenString };
                    break;
                default:
                    throw new ScriptRuntimeException(Token, $"Const token type '{Token.TokenType}' not supported!");
            }
        }

        protected override SObject DoFunction()
        {
            return new SObject(constValue);
        }

        public override string ToString()
        {
            return $"Const: {Token}";
        }
    }
}
