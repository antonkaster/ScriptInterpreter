using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Logs
{
    /// <summary>
    /// Интерфейс предоставляющий возможность вывода сообщений в среде выполенения сценария
    /// </summary>
    public interface IInterpreterLoggerWriter
    {
        /// <summary>
        /// Отпрапвить отладочное сообщение
        /// </summary>
        /// <param name="text"></param>
        public void Debug(string text);

        /// <summary>
        /// Отправить предупреждение
        /// </summary>
        /// <param name="text"></param>
        public void Warning(string text);

        /// <summary>
        /// Отправить сообщение об ошибке
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text"></param>
        public void Error(Token token, string text);

        /// <summary>
        /// Отправить сообщение об ошибке
        /// </summary>
        /// <param name="text"></param>
        public void Error(string text);
    }
}
