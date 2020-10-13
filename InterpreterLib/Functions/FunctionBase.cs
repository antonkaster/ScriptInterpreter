using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions
{
    public abstract class FunctionBase
    {
        public abstract int ArgsCount { get; }

        protected IFunctionEnvironment Environment;

        public FunctionBase(IFunctionEnvironment environment)
        {
            this.Environment = environment;
        }

        public abstract SObject GetResult(params SObject[] args);

        public override string ToString()
        {
            return $"Function: {this.GetType()} (need args: {ArgsCount})";
        }
    }
}
