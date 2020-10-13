using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.ComplexFunctions
{
    public class OnceComplexFunction : ComplexFunctionBase
    {
        public override bool IsFirstComplexFunction => true;
        public override int ArgsCount => 0;

        private bool alreadayStarted = false;

        public OnceComplexFunction(IFunctionEnvironment environment) : base(environment)
        {
        }

        public override SObject GetResult(params Func<SObject>[] args)
        {
            if (alreadayStarted)
                return new SObject(false);

            alreadayStarted = true;
            return new SObject(true);
        }
        public override bool RetryFunction() => false;

    }
}
