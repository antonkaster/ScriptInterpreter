using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions
{
    public abstract class OperationFunctionBase : FunctionBase
    {
        protected OperationFunctionBase(IFunctionEnvironment environment, int operationPriority) : base(environment)
        {
            OperationPriority = operationPriority;
        }

        public int OperationPriority { get; private set; }


    }
}
