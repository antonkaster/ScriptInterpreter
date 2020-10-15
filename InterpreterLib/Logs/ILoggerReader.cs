using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Logs
{
    public interface ILoggerReader
    {
        /// <summary>
        /// Настройки вывода сообщений
        /// </summary>
        public LoggerOptions LogOptions { get; }

        public delegate void LogMethod(string text);
        
        /// <summary>
        /// Вызывается при отладочном сообщении
        /// </summary>
        public event LogMethod OnLogDebug;

        /// <summary>
        /// Вызывается при возникновении предупреждения
        /// </summary>
        public event LogMethod OnLogWarning;

        /// <summary>
        /// Вызвается при возникновении ошибки
        /// </summary>
        public event LogMethod OnLogError;

        /// <summary>
        /// Вызывается при выводе из скрипта
        /// </summary>
        public event LogMethod OnConsoleOut;

        public delegate void LogTokenizedMethod(Token token, string text);

        /// <summary>
        /// Вызвается при возникновении ошибки, передает токен ошибки
        /// </summary>
        public event LogTokenizedMethod OnLogTokenizedError;

    }
}
