using InterpreterLib;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace InterpreterTests
{
    [TestClass]
    public class LanguageTestBase
    {
        private readonly ScriptBase langBase;
        //private SObject scriptResultOut = null;
        private IScriptInterpreter interpreter = null;

        protected string ScriptConsoleOut = string.Empty;

        public LanguageTestBase()
        {
            langBase = new ScriptBase();
            langBase.ConsoleOut += (t) => ScriptConsoleOut += t;
        }

        protected void Reset()
        {
            interpreter?.ResetEnvironment();
            ScriptConsoleOut = string.Empty;
            //scriptResultOut = null;
        }

        protected SObject Go()
        {
            return interpreter.Go();
        }

        protected SObject ParseAndGo(string scriptText)
        {
            interpreter = langBase.Parser.Parse(scriptText);
            return Go();
        }

        protected SObject ResetParseAndGo(string scriptText)
        {
            Reset();
            return ParseAndGo(scriptText);
        }

    }
}
