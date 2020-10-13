using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace InterpreterLib.Functions.ComplexFunctions
{
    public class ElseComplexFunction : ComplexFunctionBase
    {
        public ElseComplexFunction(IFunctionEnvironment environment) : base(environment)
        {
        }

        public override bool IsFirstComplexFunction => false;

        public override int ArgsCount => 0;

        public override SObject GetResult(params Func<SObject>[] args)
        {
            return new SObject(true);
        }

        public override bool RetryFunction() => false;

    }
}
