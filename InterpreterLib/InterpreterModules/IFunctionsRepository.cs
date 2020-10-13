using InterpreterLib.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.InterpreterModules
{
    /// <summary>
    /// Интерфейс предоставляющий возможность добавлять функции яыка сценария
    /// </summary>
    public interface IFunctionsRepository
    {
        /// <summary>
        /// Добавить операцию
        /// </summary>
        /// <typeparam name="T">Тип класса операции</typeparam>
        /// <param name="keyWord">Ключевое слово</param>
        /// <param name="args">Аргументыо перации</param>
        /// <returns></returns>
        public IFunctionsRepository AddOperation<T>(string keyWord, params object[] args) where T : OperationFunctionBase;

        /// <summary>
        /// Добавить функцию
        /// </summary>
        /// <typeparam name="T">Тип класса функции</typeparam>
        /// <param name="keyWord">Ключевое слово</param>
        /// <param name="args">Аргументы функции</param>
        /// <returns></returns>
        public IFunctionsRepository AddFunction<T>(string keyWord, params object[] args) where T : FunctionBase;

        /// <summary>
        /// Добавить комплексную функцию
        /// </summary>
        /// <typeparam name="T">Тип класса комплексной функции</typeparam>
        /// <param name="keyWord">Ключевое слово</param>
        /// <param name="args">Аргументы функции</param>
        /// <returns></returns>
        public IFunctionsRepository AddComplexFunction<T>(string keyWord, params object[] args) where T : ComplexFunctionBase;
    }
}
