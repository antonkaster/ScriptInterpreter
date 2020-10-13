using InterpreterLib.ScriptExceptions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Logs;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InterpreterLib.Environment
{
    /// <summary>
    /// Среда исполнения сценария
    /// </summary>
    internal class ScriptEnvironment
    {
        /// <summary>
        /// Интерфейс переменных сценария
        /// </summary>
        public IVariablesHeap Vars { get => vars; }

        /// <summary>
        /// Интерфейс логгера сценария
        /// </summary>
        public ILogger Logger { get; private set; }

        private readonly VariablesHeap vars;

        public ScriptEnvironment(ILogger logger)
        {
            vars = new VariablesHeap();
            this.Logger = logger ?? throw new ArgumentNullException("Logger can't be null!");
        }

        /// <summary>
        /// Сбрасывает среду к перевоначальному состоянию
        /// </summary>
        public void ResetEnvironment()
        {
            vars.Clear();
        }


    }
}
