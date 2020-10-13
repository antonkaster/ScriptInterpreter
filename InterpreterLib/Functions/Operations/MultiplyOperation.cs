using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.Operations
{
    public class MultiplyOperation : OperationFunctionBase
    {
        public MultiplyOperation(IFunctionEnvironment environment, int operationPriority)
            : base(environment, operationPriority)
        {
        }

        public override int ArgsCount => 2;

        public override SObject GetResult(params SObject[] args)
        {
            SObject firstArg = args[0];
            SObject secondArg = args[1];

            if (firstArg.Type == SObjectType.Numeric && secondArg.Type == SObjectType.Numeric)
            {
                return new SObject(firstArg.NumValue * secondArg.NumValue);
            }
            else if (firstArg.Type == SObjectType.String && secondArg.Type == SObjectType.Numeric)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < secondArg.NumValue; i++)
                    sb.Append(firstArg.StringValue);
                return new SObject(sb.ToString());
            }
            else if (firstArg.Type == SObjectType.Numeric && secondArg.Type == SObjectType.String)
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < firstArg.NumValue; i++)
                    sb.Append(secondArg.StringValue);
                return new SObject(sb.ToString());
            }
            throw new ArgumentException( $"Args types ({firstArg.Type}, {secondArg.Type}) not supported!");
        }
    }
}
