using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.BasicFunctions
{
    public class TestFunction : FunctionBase
    {
        public TestFunction(IFunctionEnvironment environment) : base(environment)
        {
        }

        public override int ArgsCount => 3;

        public override SObject GetResult(params SObject[] args)
        {
            return new SObject(args[0].NumValue + args[1].NumValue + args[2].NumValue);
        }
    }
}
