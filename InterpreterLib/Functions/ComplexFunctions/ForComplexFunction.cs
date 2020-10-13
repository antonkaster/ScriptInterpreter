using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace InterpreterLib.Functions.ComplexFunctions
{
    public class ForComplexFunction : ComplexFunctionBase
    {
        public override bool IsFirstComplexFunction => true;
        public override int ArgsCount => 3;

        private bool firstRun = true;

        public ForComplexFunction(IFunctionEnvironment environment) : base(environment)
        {
        }

        public override SObject GetResult(params Func<SObject>[] args)
        {
            if (firstRun)
            {
                firstRun = false;
                args[0].Invoke();
            }
            else
            {
                args[2].Invoke();
            }

            return args[1].Invoke();
        }

        public override bool RetryFunction() => true;
    }
}
