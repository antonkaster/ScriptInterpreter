using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.Functions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Logs;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpreterLib.Expressions
{
    public class OperationExpression : Expression
    {
        public FunctionBase Function { get; private set; } = null;

        private List<Expression> argsExpressions = null;

        public OperationExpression(IFunctionRuntimeControl internalControl, Token token, FunctionBase function) 
            : base(internalControl, token)
        {
            this.Function = function ?? throw new ArgumentNullException("Function can't be null!");
        }

        protected override SObject DoFunction()
        {
            if (argsExpressions == null)
            {
                argsExpressions = SubExpressions.FirstOrDefault()?.SubExpressions;
                argsExpressions ??= new List<Expression>();

                if (argsExpressions.Count != Function.ArgsCount)
                    throw new ScriptRuntimeException(Token, $"Operation <{Token}> needed {Function.ArgsCount} arguments!");
            }

            SObject[] args = new SObject[argsExpressions.Count];

            for (int i = 0; i < argsExpressions.Count; i++)
                args[i] = argsExpressions[i].GetResult();

            return Function.GetResult(args);
        }

        public override string ToString()
        {
            return "Operation: " + base.ToString();
        }
    }

}
