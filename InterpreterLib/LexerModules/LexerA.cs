using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace InterpreterLib.LexerModules
{
    internal class LexerA : ILexer
    {
        private readonly FunctionsRepository interpreter;

        public  LexerA(FunctionsRepository interpreterBox)
        {
            this.interpreter = interpreterBox ?? throw new ArgumentNullException("Interpreter box can't be null!");
        }

        public List<Token> SplitToTokenList(string scriptText)
        {
            scriptText += ";";

            List<Token> tokens = new List<Token>();
            string token = "";

            bool isStringValue = false;
            bool isLineComment = false;

            int index = 0;
            int globalIndex = 0;

            foreach (char s in scriptText)
            {
                globalIndex++;

                if (s == '\r' || s == '\n')
                {
                    if (isLineComment)
                        isLineComment = false;
                    continue;
                }

                if (isLineComment)
                    continue;

                if (token.Length == 0 && !isStringValue && s != '"')
                {
                    index = globalIndex - 1;
                    token += s;
                    continue;
                }

                if (isStringValue)
                {

                }
                else if(s == '/' && token.Length >0 && token == "/")
                {
                    ResetTemp();
                    isLineComment = true;
                    continue;
                }
                else if(LexerHelper.IsTokenSeparator(token))
                {
                    AddTokenAndResetTemp(token, index);
                }
                else if (LexerHelper.IsTokenSeparator(s))
                {
                    AddTokenAndResetTemp(token, index);
                }
                else if(GetTokenType(token) == TokenType.Operation 
                    && GetTokenType(token + s) != TokenType.Operation)
                {
                    AddTokenAndResetTemp(token, index);
                }
                else if(int.TryParse(token, out _) && !int.TryParse(token + s, out _))
                {
                    AddTokenAndResetTemp(token, index);
                }
                else if(LexerHelper.IsCorrectIdentifier(token) && !LexerHelper.IsCorrectIdentifier(token + s))
                {
                    AddTokenAndResetTemp(token, index);
                }

                if (s == '"')
                {
                    if (isStringValue)
                    {
                        if (token.Length > 0 && token.Last() == '\\')
                        {
                            token = token.Remove(token.Length - 1, 1);
                            token += '"';
                            continue;
                        }

                        isStringValue = false;

                        AddTokenByTypeAndResetTemp(TokenType.Text, token, index);
                        continue;
                    }
                    else
                    {
                        isStringValue = true;
                        continue;
                    }
                }

                token += s;
            }

            return tokens;

            void AddTokenAndResetTemp(string tokenText, int textIndex)
            {
                TokenType tokenType = GetTokenType(tokenText);

                if (tokenType != TokenType.Separator)
                {
                    if (tokenType == TokenType.Empty)
                        tokenType = TokenType.Variable;

                    tokens.Add(new Token(tokenType, tokenText, textIndex));
                }

                ResetTemp();
            }

            void AddTokenByTypeAndResetTemp(TokenType tokenType, string tokenText, int textIndex)
            {
                tokens.Add(new Token(tokenType, tokenText, textIndex));
                ResetTemp();
            }

            void ResetTemp()
            {
                token = "";
                index = globalIndex - 1;
            }
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
