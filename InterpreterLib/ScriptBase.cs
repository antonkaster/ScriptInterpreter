using InterpreterLib.Environment;
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
        public delegate void ScriptOutMethod(string text);
        public delegate void ScriptErrorOutMethod(Token token, string text);
        public delegate void StepExecMethod(Token token);
        public event ScriptOutMethod ConsoleOut;
        public event ScriptOutMethod DebugOut;
        public event ScriptErrorOutMethod ErrorOut;

        /// <summary>
        /// параметры вывода сообщений среды сценария
        /// </summary>
        public LoggerOptions LogOptions { get => logger.LoggerOptions; }

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

        private readonly ILogger logger;
        private readonly FunctionsRepository functions;
        private readonly ScriptEnvironment scriptEnvironment;
        private readonly ParserFabric parser;

        public ScriptBase()
        {
            logger = new BasicLogger(
                    (t) => WriteConsoleOut(t),
                    (t) => WriteDebugOut($"{t}"),
                    (t) => WriteDebugOut($"\r\nWarning: {t}\r\n"),
                    (s,t) => WriteErrorOut(s,$"\r\nError: {t}\r\n")
                );

            scriptEnvironment = new ScriptEnvironment(logger);
            functions = new FunctionsRepository(new FunctionEnvironment(scriptEnvironment));
            InitFunctions();

            parser = new ParserFabric(logger, functions, scriptEnvironment);
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

        private void WriteConsoleOut(string text)
        {
            if (ConsoleOut == null)
                Debug.Print(text);
            else
                ConsoleOut.Invoke(text);
        }
        
        private void WriteDebugOut(string text)
        {
            if (DebugOut == null)
                Debug.Print($"Debug: " + text);
            else
                DebugOut.Invoke(text);
        }

        private void WriteErrorOut(Token token, string text)
        {
            WriteDebugOut(text);

            if (ErrorOut == null)
                Debug.Print($"Error: " + text);
            else
                ErrorOut.Invoke(token, text);
        }

    }
}
