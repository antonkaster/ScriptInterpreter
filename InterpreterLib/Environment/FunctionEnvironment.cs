using InterpreterLib.ScriptObjects;
using InterpreterLib.Logs;
using System;
using System.Collections.Generic;
using System.Text;
using InterpreterLib.ScriptExceptions;

namespace InterpreterLib.Environment
{
    /// <summary>
    /// Среда выполнения для функции
    /// </summary>
    internal class FunctionEnvironment : IFunctionEnvironment
    {
        public IScriptLoggerWriter Logger { get; }

        private readonly ScriptEnvironment environment;

        public FunctionEnvironment(ScriptEnvironment environment)
        {
            this.environment = environment ?? throw new ArgumentNullException("EnvironmentBox can't be null!");
            Logger = environment.ScriptLoger;
        }

        public void StopScript()
        {
            throw new ScriptStopException(ScriptStopReason.InFunctionStop, new SObject());
        }

        public void StopScriptAndReturnValue(SObject obj)
        {
            throw new ScriptStopException(ScriptStopReason.InFunctionStop, obj);
        }
    }
}
