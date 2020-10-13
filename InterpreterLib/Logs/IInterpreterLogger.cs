using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Logs
{
    /// <summary>
    /// Интерфейс предоставляющий возможность вывода сообщений в среде выполенения сценария
    /// </summary>
    public interface IInterpreterLogger
    {
        /// <summary>
        /// Настройки вывода сообщений
        /// </summary>
        public LoggerOptions LoggerOptions { get; }

        /// <summary>
        /// Отпрапвить отладочное сообщение
        /// </summary>
        /// <param name="text"></param>
        public void LogDebug(string text);

        /// <summary>
        /// Отправить предупреждение
        /// </summary>
        /// <param name="text"></param>
        public void LogWarning(string text);

        /// <summary>
        /// Отправить сообщение об ошибке
        /// </summary>
        /// <param name="token"></param>
        /// <param name="text"></param>
        public void LogTokenizedError(Token token, string text);

        /// <summary>
        /// Отправить сообщение об ошибке
        /// </summary>
        /// <param name="text"></param>
        public void LogError(string text);
    }
}
