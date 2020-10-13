using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.Operations
{
    public class SimpleBooleanOperation : OperationFunctionBase
    {
        public override int ArgsCount => 2;

        private readonly Func<SObject, SObject, bool> logicFunc;

        public SimpleBooleanOperation(IFunctionEnvironment environment, int operationPriority,  Func<SObject, SObject, bool> logicFunc)
            : base(environment, operationPriority)
        {
            this.logicFunc = logicFunc;
        }

        public override SObject GetResult(params SObject[] args)
        {
            return new SObject(logicFunc.Invoke(args[0], args[1]));
        }
    }
}
