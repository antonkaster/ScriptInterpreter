using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.Functions;
using InterpreterLib.Functions.ComplexFunctions;
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
    public class ComplexFunctionExpression : Expression
    {
        public ComplexFunctionBase Function { get; private set; } = null;

        private List<Expression> argsExpressions = null;

        public ComplexFunctionExpression(IFunctionRuntimeControl internalControl, Token token, ComplexFunctionBase function) 
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
                    throw new ScriptRuntimeException(Token, $"Complex function needed {Function.ArgsCount} arguments!");
            }

            Func<SObject>[] argsFunc = argsExpressions
                .Select(f => new Func<SObject>(() => f.GetResult()))
                .ToArray();

            SObject funcResult = Function.GetResult(argsFunc);

            Expression bodyExpression = SubExpressions
                        .Skip(1)
                        .First();

            if (funcResult?.BoolValue == true)
                bodyExpression.GetResult();

            while(Function.RetryFunction())
            {
                funcResult = Function.GetResult(argsFunc);

                if (funcResult?.BoolValue == true)
                    bodyExpression.GetResult();
                else
                    break;
            }

            return funcResult;
        }

        public override string ToString()
        {
            return $"ComplexFunction: {base.ToString()}";
        }

    }
}
