﻿using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.Expressions;
using InterpreterLib.Functions.BasicFunctions;
using InterpreterLib.Functions.ComplexFunctions;
using InterpreterLib.Functions.Operations;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.LexerModules;
using InterpreterLib.Logs;
using InterpreterLib.ParserModules;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using InterpreterLib.Functions;

namespace InterpreterLib
{
    /// <summary>
    /// Основной класс интерпретатора сценария
    /// </summary>
    public class ScriptBase
    {
        /// <summary>
        /// Обеспечивает вывод
        /// </summary>
        public ILoggerReader Logger { get => logger; }

        /// <summary>
        /// Парсер сценариев
        /// </summary>
        public IScriptParser Parser { get => parser; }

        /// <summary>
        /// переменные среды сценариев
        /// </summary>
        public IVariablesHeap Vars { get => scriptEnvironment.Vars; }
        
        /// <summary>
        /// Функции языка сценария
        /// </summary>
        public IFunctionsRepository Functions { get => functions; }

        private readonly ScriptLogger logger;
        private readonly FunctionsRepository functions;
        private readonly ScriptEnvironment scriptEnvironment;
        private readonly ParserFactory parser;

        public ScriptBase()
        {
            logger = new ScriptLogger();

            scriptEnvironment = new ScriptEnvironment(logger);
            functions = new FunctionsRepository(new FunctionEnvironment(scriptEnvironment));
            InitFunctions();

            parser = new ParserFactory(logger, functions, scriptEnvironment);
        }

        private void InitFunctions()
        {
            Functions
                .AddOperation<VariableSetOperation>("=", 0)
                .AddOperation<PlusOperation>("+", 1)
                .AddOperation<MinusOperation>("-", 1)
                .AddOperation<MultiplyOperation>("*", 2)
                .AddOperation<DivisionOperation>("/", 2)
                .AddOperation<SimpleBooleanOperation>("||", 2, new Func<SObject,SObject,bool>((a,b) => a.BoolValue || b.BoolValue))
                .AddOperation<SimpleBooleanOperation>("&&", 2, new Func<SObject, SObject, bool>((a,b) => a.BoolValue && b.BoolValue))
                .AddOperation<SimpleBooleanOperation>("==", 2, new Func<SObject, SObject, bool>((a,b) => a.Equals(b)))
                .AddOperation<SimpleBooleanOperation>("!=", 2, new Func<SObject, SObject, bool>((a,b) => !a.Equals(b)))
                .AddOperation<NumericSimpleBooleanOperation>(">", 2, new Func<SObject, SObject, bool>((a,b) => a.NumValue > b.NumValue))
                .AddOperation<NumericSimpleBooleanOperation>("<", 2, new Func<SObject, SObject, bool>((a,b) => a.NumValue < b.NumValue))
                .AddOperation<NumericSimpleBooleanOperation>("<=", 2, new Func<SObject, SObject, bool>((a,b) => a.NumValue <= b.NumValue))
                .AddOperation<NumericSimpleBooleanOperation>(">=", 2, new Func<SObject, SObject, bool>((a,b) => a.NumValue >= b.NumValue))
                .AddOperation<NotOperation>("!", 3);

            Functions
                .AddComplexFunction<IfComplexFunction>("if")
                .AddComplexFunction<ElseIfComplexFunction>("elseif")
                .AddComplexFunction<ElseComplexFunction>("else")
                .AddComplexFunction<OnceComplexFunction>("once")
                .AddComplexFunction<WhileComplexFunction>("while")
                .AddComplexFunction<ForComplexFunction>("for");

            Functions
                .AddFunction<ConstFunction>("true", new SObject(true))
                .AddFunction<ConstFunction>("false", new SObject(false))
                .AddFunction<PrintFunction>("print", new Func<string, string>((t) => t))
                .AddFunction<PrintFunction>("printline", new Func<string, string>(t => t + "\r\n"))
                .AddFunction<PauseFunction>("pause")
                .AddFunction<StopFunction>("stop")
                .AddFunction<ReturnFunction>("return");

            Functions
                .AddFunction<TestFunction>("test");
        }

        /// <summary>
        /// Получить список операций языка
        /// </summary>
        /// <returns></returns>
        public string[] GetOperationsList() => functions.GetOperationsList();

        /// <summary>
        /// Получить список функций языка
        /// </summary>
        /// <returns></returns>
        public string[] GetFunctionsList() => functions.GetFunctionsList();

        /// <summary>
        /// Получить список комплексных функций языка
        /// </summary>
        /// <returns></returns>
        public string[] GetComplexFunctionsList() => functions.GetComplexFunctionsList();

    }
}
