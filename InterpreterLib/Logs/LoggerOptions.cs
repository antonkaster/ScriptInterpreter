using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Logs
{
    /// <summary>
    /// Настройки вывода информации среды выполнения сценария
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// Разрешить выводить отладочные сообщения
        /// Включение данного параметра может заметно снизить скорость выполнения сценария
        /// </summary>
        public bool EnableDebug { get; set; } = false;

        /// <summary>
        /// Разрешить выводить предупреждения
        /// </summary>
        public bool EnableWarning { get; set; } = true;

        /// <summary>
        /// Разрешить выводить ошибки
        /// </summary>
        public bool EnableError { get; set; } = true;

        /// <summary>
        /// Разрешить выводить сообщения сценария
        /// </summary>
        public bool EnableConsoleOut { get; set; } = true;

    }
}
