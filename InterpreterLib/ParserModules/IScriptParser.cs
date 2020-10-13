using InterpreterLib.InterpreterModules;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.ParserModules
{
    public interface IScriptParser
    {
        public IScriptInterpreter Parse(string scriptText);
        public IScriptInterpreter LoadFromFileAndParse(string fileName);
    }
}
