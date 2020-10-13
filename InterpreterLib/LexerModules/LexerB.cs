using InterpreterLib.InterpreterModules;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace InterpreterLib.LexerModules
{
    internal class LexerB : ILexer
    {
        
        private readonly FunctionsRepository interpreter;

        private List<Token> tokens;
        private int pointer;
        private int length;
        private int tokenStart;
        private string script;
        private string token;
        private char currChar;

        public LexerB(FunctionsRepository interpreter)
        {
            this.interpreter = interpreter ?? throw new ArgumentNullException("InterpreterBox can't be null!");
        }

        public List<Token> SplitToTokenList(string scriptText)
        {
            scriptText += ";";
            tokens = new List<Token>();
            script = scriptText;
            pointer = -1;
            token = "";
            length = script.Length;

            while (MoveNext())
            {
                if (token == "/" && currChar == '/')
                    MakeComment();

                if (token == "\"")
                    MakeTextToken();

                if (LexerHelper.IsNumeric(token))
                    MakeNumericToken();

                if(LexerHelper.IsTokenSeparator(token) || (token.Length > 0 && IsEndOfToken()))
                    AddTokenAndResetTemp(GetTokenType(token));

                token += currChar;
            }

            return tokens;
        }

        private void MakeComment()
        {
            token = "";

            do
            {
            }
            while (MoveNext() && currChar != '\r');
            tokenStart = pointer;
        }

        private void MakeTextToken()
        {
            token = "";

            do
            {
                if (currChar == '"')
                    break;
                token += currChar;
            }
            while (MoveNext());

            AddTokenAndResetTemp(TokenType.Text);
            MoveNext();
        }

        private void MakeNumericToken()
        {
            tokenStart = pointer;

            while (LexerHelper.IsNumeric(currChar))
            {
                token += currChar;
                MoveNext();
            }
            tokenStart--;
            AddTokenAndResetTemp(TokenType.Numeric);
        }

        private bool IsEndOfToken()
        {
            if (LexerHelper.IsTokenSeparator(currChar))
                return true;
            if (LexerHelper.IsCorrectIdentifier(token) != LexerHelper.IsCorrectIdentifier(currChar))
                return true;
            if (LexerHelper.IsNumeric(token) != LexerHelper.IsNumeric(currChar))
                return true;
            if (GetTokenType(token + currChar) != TokenType.Operation 
                && GetTokenType(token) == TokenType.Operation 
                && GetTokenType(currChar) == TokenType.Operation)
                return true;

            return false;
        }


        private bool MoveNext()
        {
            pointer++;

            if (pointer >= length)
                return false;

            currChar = script[pointer];
            return true;
        }

        private void AddTokenAndResetTemp(TokenType type)
        {
            if(type == TokenType.Empty)
            {
                if (LexerHelper.IsCorrectIdentifier(token))
                    type = TokenType.Variable;
            }

            Token t = new Token(type, token, tokenStart);
            tokens.Add(t);
            token = "";
            tokenStart = pointer;
        }


        private TokenType GetTokenType(char c) => GetTokenType(c.ToString());
            
        private TokenType GetTokenType(string text)
        {
            text = text?.ToLower();

            if (LexerHelper.IsStartOfExpression(text))
                return TokenType.StartExpr;
            if (LexerHelper.IsEndOfExpression(text))
                return TokenType.EndExpr;
            if (interpreter.GetComplexFunctionsList().Contains(text))
                return TokenType.ComplexFunction;
            if (interpreter.GetOperationsList().Contains(text))
                return TokenType.Operation;
            if (interpreter.GetFunctionsList().Contains(text))
                return TokenType.Function;
            if (double.TryParse(text, out _))
                return TokenType.Numeric;
            if (LexerHelper.IsTokenSeparator(text))
                return TokenType.Separator;

            return TokenType.Empty;
        }

    }
}
