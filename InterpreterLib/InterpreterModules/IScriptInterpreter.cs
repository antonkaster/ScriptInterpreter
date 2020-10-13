using InterpreterLib.Environment;
using InterpreterLib.Expressions;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterLib.InterpreterModules
{
    /// <summary>
    /// Интерфейс предоставляющий внешнему коду доступ к функциям интерпретатора сценария
    /// </summary>
    public interface IScriptInterpreter
    {
        public delegate void ScriptFinishedMethod(SObject obj);

        /// <summary>
        /// Событие, вызывается при завершении работы сценария, возвращает результат выполнения сценария
        /// </summary>
        public event ScriptFinishedMethod ScriptFinished;

        /// <summary>
        /// Хранилище переменных среды выполнения сценария
        /// </summary>
        public IVariablesHeap Vars { get; }

        /// <summary>
        /// Интерфейс для внешнего управления сценарием
        /// </summary>
        public IExternalRuntimeControl RuntimeControl { get; }

        /// <summary>
        /// Запустить подготовленный сценарий в синхронном режиме
        /// </summary>
        /// <param name="stepByStep">Выполнить в пошаговом реиме</param>
        /// <param name="resetEnvironment">Сбросить состояние среды перед выполнением</param>
        /// <returns>Результат выполнения сценария</returns>
        public SObject Go(bool stepByStep = false, bool resetEnvironment = false);

        /// <summary>
        /// Запустить подготовленный сценарий в асинхронном режиме
        /// </summary>
        /// <param name="stepByStep">Выполнить в пошаговом режиме</param>
        /// <param name="resetEnvironment">Сбросить состояние среды перед выполнением</param>
        /// <returns>Задача выполнения сценария</returns>
        public Task<SObject> GoAsync(bool stepByStep, bool resetEnvironment = true);

        /// <summary>
        /// Разрешает сделать следующий шаг в режиме пошагового выполнения сценария
        /// </summary>
        public void DoStep();

        /// <summary>
        /// Остановить работу сценария
        /// </summary>
        public void Stop();

        /// <summary>
        /// Сбросить среду выполнения сценария
        /// </summary>
        public void ResetEnvironment();

        /// <summary>
        /// Получить дерево выражений сценария
        /// </summary>
        /// <returns></returns>
        public Expression GetRootExpression();

        /// <summary>
        /// Получить список токенов сценария
        /// </summary>
        /// <returns></returns>
        public List<Token> GetTokens();
    }
}
