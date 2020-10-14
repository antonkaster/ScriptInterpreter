using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Environment
{
    /// <summary>
    /// Интерфейс для внутреннего управления выполнением сценария
    /// </summary>
    public interface IFunctionRuntimeControl
    {
        /// <summary>
        /// Отправляет сигнал о том что начинаетя новый шаг выполнения сценария
        /// </summary>
        /// <param name="token">Токен нового шага</param>
        public void WaitGoNextStepOrExit(Token token);
    }
}
