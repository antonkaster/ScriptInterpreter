using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Tokens
{
    public class Token
    {
        public int StartIndex { get; private set; } = -1;
        public int TokenStringLenght
        {
            get => TokenType switch
                {
                    TokenType.Text => TokenString.Length + 2,
                    TokenType.Empty => 0,
                    _ => TokenString.Length
                };
        }
        public string TokenString { get; private set; }
        public TokenType  TokenType { get; private set; }

        public Token()
        {
            TokenType = TokenType.Empty;
            TokenString = "";
            StartIndex = -1;
        }

        public Token(TokenType tokenType, string tokenText, int textindex)
        {
            TokenString = tokenText;
            TokenType = tokenType;
            StartIndex = textindex;
        }

        public override string ToString()
        {
            return $"[{TokenType}] '{TokenString}' ({StartIndex})";
        }
    }
}
