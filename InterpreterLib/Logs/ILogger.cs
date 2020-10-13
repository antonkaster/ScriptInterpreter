using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Logs
{
    /// <summary>
    /// Интерфес глобального логгера
    /// </summary>
    public interface ILogger : IInterpreterLogger, IScriptLogger
    {
    }
}
