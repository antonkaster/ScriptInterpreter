using InterpreterLib.Environment;
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
    public class VariableExpression : Expression
    {
        private bool argumentsChecked = false;
        private ArgumentsExpression arguments = null;

        private readonly IVariablesHeap vars;

        public VariableExpression(IFunctionRuntimeControl internalControl, Token token, IVariablesHeap vars) 
            : base(internalControl, token)
        {
            this.vars = vars ?? throw new ArgumentNullException("Vars can't be null!");
        }

        protected override SObject DoFunction()
        {
            SObject result = vars[Token.TokenString];

            if (!argumentsChecked)
            {
                if (SubExpressions?.FirstOrDefault() as ArgumentsExpression != null)
                    arguments = SubExpressions[0] as ArgumentsExpression;

                argumentsChecked = true;
            }

            if (arguments != null)
            {
                string ident = arguments.GetResult()?.StringValue;
                return result.Vars[ident];
            }
            else
            {
                return result;
            }
        }

        public override string ToString()
        {
            return $"Var: {Token.TokenString}";
        }
    }
}
