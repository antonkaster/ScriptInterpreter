using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InterpreterLib.Functions.BasicFunctions
{
    public class PauseFunction : FunctionBase
    {
        public PauseFunction(IFunctionEnvironment environment) : base(environment)
        {
        }

        public override int ArgsCount => 1;

        public override SObject GetResult(params SObject[] args)
        {
            Thread.Sleep((int)args[0].NumValue);
            return new SObject();
        }
    }
}
