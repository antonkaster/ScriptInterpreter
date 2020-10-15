using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Logs
{
    /// <summary>
    /// Интерфейс предоставляющий возможность вывода сообщений из сценария
    /// </summary>
    public interface IScriptLoggerWriter
    {
        /// <summary>
        /// Отправить сообщение сценария
        /// </summary>
        /// <param name="text"></param>
        public void ConsoleOut(string text);

    }
}
