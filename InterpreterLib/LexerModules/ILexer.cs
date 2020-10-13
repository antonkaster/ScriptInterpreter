using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.LexerModules
{
    internal interface ILexer
    {
        public List<Token> SplitToTokenList(string scriptText);

    }
}
