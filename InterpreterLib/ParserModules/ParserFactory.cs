using InterpreterLib.Environment;
using InterpreterLib.Expressions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.LexerModules;
using InterpreterLib.Logs;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace InterpreterLib.ParserModules
{
    /// <summary>
    /// Фабрика генерирующая экземпляры интерпретатора на основе текста сценария
    /// </summary>
    public class ParserFactory : IScriptParser
    {
        private readonly FunctionsRepository functions;
        private readonly ScriptEnvironment scriptEnvironment;
        private readonly IInterpreterLogger logger;

        internal ParserFactory(IInterpreterLogger logger , FunctionsRepository functions, ScriptEnvironment scriptEnvironment)
        {
            this.functions = functions ?? throw new ArgumentNullException("Functions repository can't be null!");
            this.scriptEnvironment = scriptEnvironment ?? throw new ArgumentNullException("Script environment can't be null!");
            this.logger = logger ?? throw new ArgumentNullException("Logger can't be null!");
        }


        /// <summary>
        /// Подготавливает сценрий к запуску
        /// </summary>
        /// <param name="scriptText">Текст сценария</param>
        /// <returns>Текущий экземпляр интерпретатора</returns>
        public IScriptInterpreter Parse(string scriptText)
        {
            Expression rootExpression = null;
            List<Token> tokens = null;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();

            try
            {
                ILexer lexer = new LexerB(functions);
                tokens = lexer.SplitToTokenList(scriptText);

                stopwatch.Stop();

                logger.LogDebug($"Tokenized in {stopwatch.Elapsed}");
            }
            catch (Exception ex)
            {
                logger.LogError($"Tokenize error: " + ex.Message);
                throw new TokenizeException($"Tokenize error: " + ex.Message, ex);
            }

            try
            {
                stopwatch.Restart();

                ScriptRuntimeControl runtimeControl = new ScriptRuntimeControl(logger);

                ParserA parser = new ParserA(functions, scriptEnvironment.Vars, runtimeControl);
                rootExpression = parser.MakeTree(tokens);

                stopwatch.Stop();

                logger.LogDebug($"Parsed in {stopwatch.Elapsed}");
                return new Interpreter(scriptEnvironment, runtimeControl, rootExpression, tokens);
            }
            catch (Exception ex)
            {
                logger.LogError($"Parse error: " + ex.Message);
                throw new ParseException($"Parse error: " + ex.Message, ex);
            }

        }

        public IScriptInterpreter LoadFromFileAndParse(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException($"File '{fileName}' not found!");

            return Parse(File.ReadAllText(fileName));
        }

    }
}
