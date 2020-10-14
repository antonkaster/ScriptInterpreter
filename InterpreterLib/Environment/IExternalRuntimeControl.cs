using InterpreterLib.ScriptObjects;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InterpreterLib.Environment
{
    /// <summary>
    /// Интерфейс для внешнего управления выполнением сценария
    /// </summary>
    public interface IExternalRuntimeControl
    {
        public delegate void StepExecMethod(Token token);

        /// <summary>
        /// Вызывается перед началом выполнения шага в пошаговом режиме
        /// </summary>
        public event StepExecMethod BeforeStepExec;

        /// <summary>
        /// Вызывается после нчала выполнения шага в пошаговом режиме
        /// </summary>
        public event StepExecMethod StepExec;

        /// <summary>
        /// Флаг, указывает выполняется ли сейчас сценарий
        /// </summary>
        public bool ScriptInProcess { get;  }

        /// <summary>
        /// Флаг, указывает включен ли режим пошагового выполнения
        /// </summary>
        public bool StepByStepExecMode { get;  }

        /// <summary>
        /// Токен для отмены выполнения сценария
        /// </summary>
        public CancellationToken CancellationToken { get; }

        /// <summary>
        /// Запускает сценарий
        /// </summary>
        /// <param name="stepByStepMode">Режим пошагового выполнения</param>
        //public void ScriptStart(bool stepByStepMode = false);

        /// <summary>
        /// Останавливает сценарий
        /// </summary>
        public void Stop();

        /// <summary>
        /// Разрешает выполнить следующий шаг в режиме пошагового выполнения
        /// </summary>
        public void EnableNextStep();
    }
}
