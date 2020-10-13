using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.Operations
{
    public class NotOperation : OperationFunctionBase
    {
        public override int ArgsCount => 1;


        public NotOperation(IFunctionEnvironment environment, int operationPriority)
            : base(environment, operationPriority)
        {
        }

        public override SObject GetResult(params SObject[] args)
        {
            return new SObject(!args[0].BoolValue);
        }
    }
}
