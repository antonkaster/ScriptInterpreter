using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.Operations
{
    public class DivisionOperation : OperationFunctionBase
    {
        public DivisionOperation(IFunctionEnvironment environment, int operationPriority)
            : base(environment, operationPriority)
        {
        }

        public override int ArgsCount => 2;

        public override SObject GetResult(params SObject[] args)
        {
            return new SObject(args[0].NumValue / args[1].NumValue);

        }
    }
}
