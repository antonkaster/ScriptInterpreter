using InterpreterLib.ScriptObjects;
using InterpreterLib.Logs;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Environment
{
    /// <summary>
    /// Интерфейс окружения для функции
    /// </summary>
    public interface IFunctionEnvironment
    {
        /// <summary>
        /// Логгер для вывода информации
        /// </summary>
        public IScriptLogger Logger { get; }

        /// <summary>
        /// Посылает сигнал остановки сценария
        /// </summary>
        public void StopScript();

        /// <summary>
        /// Посылает сигнал остановки сценария и передает значение для возврата
        /// </summary>
        /// <param name="obj">Значение для возврата</param>
        public void StopScriptAndReturnValue(SObject obj);
    }
}
