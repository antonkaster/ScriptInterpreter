using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.BasicFunctions
{
    public class ConstFunction : FunctionBase
    {
        public override int ArgsCount => 0;

        private readonly SObject constObj;

        public ConstFunction(IFunctionEnvironment environment, SObject obj) : base(environment)
        {
            constObj = obj;
        }

        public override SObject GetResult(params SObject[] args)
        {
            return constObj;
        }
    }
}
