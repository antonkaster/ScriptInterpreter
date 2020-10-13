using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.Operations
{
    public class NumericSimpleBooleanOperation : SimpleBooleanOperation
    {
        public NumericSimpleBooleanOperation(IFunctionEnvironment environment, int operationPriority, Func<SObject, SObject, bool> logicFunc)
            : base(environment, operationPriority, logicFunc)
        {

        }

        public override SObject GetResult(params SObject[] args)
        {
            if (args[0].Type != SObjectType.Numeric)
                throw new ArgumentException($"Wrong first arg type!");
            if (args[1].Type != SObjectType.Numeric)
                throw new ArgumentException($"Wrong second arg type!");

            return base.GetResult(args);
        }
    }
}
