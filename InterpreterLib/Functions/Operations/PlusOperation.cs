using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.Operations
{
    public class PlusOperation : OperationFunctionBase
    {
        public PlusOperation(IFunctionEnvironment environment, int operationPriority)
            : base(environment, operationPriority)
        {
        }

        public override int ArgsCount => 2;

        public override SObject GetResult(params SObject[] args)
        {
            if (args[0].Type == SObjectType.Numeric && args[1].Type == SObjectType.Numeric)
            {
                return new SObject(args[0].NumValue + args[1].NumValue);
            }
            else
            {
                return new SObject(args[0].StringValue + args[1].StringValue);
            }

        }
    }
}
