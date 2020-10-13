using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.Tokens
{
    public enum TokenType
    {
        Empty,
        Operation,
        Function,
        ComplexFunction,
        StartExpr,
        EndExpr,
        Numeric,
        Text,
        Variable,
        Separator
        
    }
}
