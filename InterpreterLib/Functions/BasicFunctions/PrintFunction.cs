using InterpreterLib.Environment;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Functions.BasicFunctions
{
    public class PrintFunction : FunctionBase
    {
        public override int ArgsCount => 1;
        private readonly Func<string, string> print;

        public PrintFunction(IFunctionEnvironment environment, Func<string,string> printFunc) : base(environment)
        {
            print = printFunc;
        }

        public override SObject GetResult(params SObject[] args)
        {
            SObject val = args[0];
            Environment.Logger.ConsoleOut(print?.Invoke(val.ToString()));
            return val;
        }
    }
}
