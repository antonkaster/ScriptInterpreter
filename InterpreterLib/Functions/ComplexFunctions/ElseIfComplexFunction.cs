using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.ComplexFunctions
{
    public class ElseIfComplexFunction : ComplexFunctionBase
    {
        public ElseIfComplexFunction(IFunctionEnvironment environment) : base(environment)
        {
        }

        public override bool IsFirstComplexFunction => false;
        public override int ArgsCount => 1;

        public override SObject GetResult(params Func<SObject>[] args)
        {
            return new SObject(args[0].Invoke());
        }
        public override bool RetryFunction() => false;

    }
}
