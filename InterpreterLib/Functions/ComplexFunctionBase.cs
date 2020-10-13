using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpreterLib.Functions
{
    public abstract class ComplexFunctionBase
    {
        public abstract int ArgsCount { get; }
        public abstract bool IsFirstComplexFunction { get; }

        protected IFunctionEnvironment Environment;

        public ComplexFunctionBase(IFunctionEnvironment environment)
        {
            this.Environment = environment;
        }

        public abstract bool RetryFunction();

        public abstract SObject GetResult(params Func<SObject>[] funcArgs);
    }
}
