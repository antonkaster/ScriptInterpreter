using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.Operations
{
    public class VariableSetOperation : OperationFunctionBase
    {
        public VariableSetOperation(IFunctionEnvironment environment, int operationPriority) 
            : base(environment, operationPriority)
        {
        }

        public override int ArgsCount => 2;

        public override SObject GetResult(params SObject[] args)
        {
            args[0].SetValue(args[1]);
            return args[0];
        }
    }
}
