using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.BasicFunctions
{
    public class ReturnFunction : FunctionBase
    {
        public ReturnFunction(IFunctionEnvironment environment) : base(environment)
        {
        }

        public override int ArgsCount => 1;

        public override SObject GetResult(params SObject[] args)
        {
            throw new ScriptStopException(ScriptStopReason.InFunctionStop, args[0]);
        }
    }
}
