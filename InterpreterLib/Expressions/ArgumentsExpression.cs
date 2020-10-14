using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Logs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Expressions
{
    public class ArgumentsExpression : Expression
    {

        public ArgumentsExpression(IFunctionRuntimeControl internalControl) 
            : base(internalControl)
        {
        }

        public override string ToString()
        {
            return $"Arguments: {SubExpressions.Count}";
        }
    }
}
