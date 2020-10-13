using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.BasicFunctions
{
    public class StopFunction : FunctionBase
    {
        public StopFunction(IFunctionEnvironment environment) : base(environment)
        {
        }

        public override int ArgsCount => 0;

        public override SObject GetResult(params SObject[] args)
        {
            throw new ScriptStopException(ScriptStopReason.InFunctionStop);
        }
    }
}
