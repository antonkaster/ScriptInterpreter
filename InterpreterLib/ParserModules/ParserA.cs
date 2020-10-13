using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.Expressions;
using InterpreterLib.Functions;
using InterpreterLib.Functions.Operations;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Logs;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpreterLib.ParserModules
{
    /// <summary>
    /// Парсер сценария (1)
    /// </summary>
    internal class ParserA
    {
        private readonly IVariablesHeap vars;
        private readonly IInternalRuntimeControl runtimeControl;
        private readonly FunctionsRepository functions;

        public ParserA(FunctionsRepository functions, IVariablesHeap vars, IInternalRuntimeControl runtimeControl)
        {
            this.functions = functions ?? throw new ArgumentNullException("Interpreter box can't be null!");
            this.vars = vars ?? throw new ArgumentNullException("Vars can't be null!");
            this.runtimeControl = runtimeControl ?? throw new ArgumentNullException("Runtime control can't be null!");
        }

        public Expression MakeTree(List<Token> tokens)
        {
            Expression root = MakeExpressionRecursive(tokens, ref listPointer);
            return root;
        }

        private int listPointer = 0;
        private Expression MakeExpressionRecursive(List<Token> tokens, ref int pointer)
        {
            Expression expression = new Expression(runtimeControl);

            for(;; pointer++)
            {
                if (pointer >= tokens.Count)
                {
                    MakeTreeInExpression(expression);
                    return expression;
                }

                Token token = tokens[pointer];

                switch (token.TokenType)
                {
                    case TokenType.StartExpr:
                        pointer++;
                        Expression subExp = MakeExpressionRecursive(tokens, ref pointer);
                        ArgumentsExpression argumentException = new ArgumentsExpression(runtimeControl);
                        argumentException.AddExpressions(subExp.SubExpressions);
                        expression.AddExpression(argumentException);
                        continue;
                    case TokenType.EndExpr:
                        MakeTreeInExpression(expression);
                        return expression;
                    case TokenType.ComplexFunction:
                        ComplexFunctionBase complexFunction = functions.GetComplexFunction(token.TokenString);
                        expression.AddExpression(new ComplexFunctionExpression(runtimeControl, token, complexFunction));
                        break;
                    case TokenType.Operation:
                        OperationFunctionBase operationFunction = functions.GetOperation(token.TokenString);
                        expression.AddExpression(new OperationExpression(runtimeControl, token, operationFunction));
                        break;
                    case TokenType.Function:
                        FunctionBase function = functions.GetFunction(token.TokenString);
                        expression.AddExpression(new FunctionExpression(runtimeControl, token, function));
                        break;
                    case TokenType.Numeric:
                        expression.AddExpression(new ConstExpression(runtimeControl, token));
                        break;
                    case TokenType.Text:
                        expression.AddExpression(new ConstExpression(runtimeControl, token));
                        break;
                    case TokenType.Variable:
                        expression.AddExpression(new VariableExpression(runtimeControl, token, vars));
                        break;
                    case TokenType.Separator:
                        break;
                    default:
                        throw new ParseException($"Token type '{token.TokenType}' not supported!");
                }
            }

        }

        private void MakeTreeInExpression(Expression expression)
        {
            if (expression.SubExpressions.Count < 2)
                return;

            int maxOperPriority = -1;
            int maxPriorityOperationIndex = -1;

            for (int i = 0; i < expression.SubExpressions.Count; i++)
            {
                Expression currExpr = expression.SubExpressions[i];

                if (currExpr is ComplexFunctionExpression)
                {
                    Expression funcExpression = new Expression(runtimeControl);
                    funcExpression.AddExpression(currExpr);

                    Expression argumentExpression = new ArgumentsExpression(runtimeControl);

                    if ((currExpr as ComplexFunctionExpression).Function.ArgsCount > 0)
                    {
                        argumentExpression = expression.SubExpressions[i + 1];
                        expression.SubExpressions.RemoveAt(i + 1);
                    }
                    currExpr.AddExpression(argumentExpression);

                    Expression bodyExpression = expression.SubExpressions[i + 1];
                    currExpr.AddExpression(bodyExpression);
                    expression.SubExpressions.Remove(bodyExpression);

                    expression.SubExpressions.Remove(currExpr);

                    if ((currExpr as ComplexFunctionExpression).Function.IsFirstComplexFunction)
                    {
                        expression.SubExpressions.Insert(i, funcExpression);
                    }
                    else
                    {
                        expression.SubExpressions[i - 1].AddExpression(currExpr);
                    }

                    MakeTreeInExpression(funcExpression);
                    MakeTreeInExpression(bodyExpression);
                    MakeTreeInExpression(expression);
                    return;
                }
                else if (currExpr is OperationExpression)
                {
                    int currOperPriority = ((currExpr as OperationExpression).Function as OperationFunctionBase).OperationPriority;
                    if (currOperPriority > maxOperPriority)
                    {
                        maxOperPriority = currOperPriority;
                        maxPriorityOperationIndex = i;
                    }
                }
                else if (currExpr is FunctionExpression)
                {
                    Expression funcExpression = new Expression(runtimeControl);
                    funcExpression.AddExpression(currExpr);

                    Expression argumentExpression = new ArgumentsExpression(runtimeControl);

                    if ((currExpr as FunctionExpression).Function.ArgsCount > 0)
                    {
                        argumentExpression = expression.SubExpressions[i + 1];
                        expression.SubExpressions.RemoveAt(i + 1);
                    }
                    currExpr.AddExpression(argumentExpression);

                    expression.SubExpressions[i] = funcExpression;

                    MakeTreeInExpression(funcExpression);
                    MakeTreeInExpression(expression);
                    return;
                }
                else if (currExpr is VariableExpression)
                {
                    Expression funcExpression = new Expression(runtimeControl);
                    funcExpression.AddExpression(currExpr);

                    if (expression.SubExpressions.Count > i+1 && expression.SubExpressions[i + 1] is ArgumentsExpression)
                    {
                        currExpr.AddExpression(expression.SubExpressions[i + 1]);
                        expression.SubExpressions.RemoveAt(i + 1);
                    }

                    expression.SubExpressions[i] = funcExpression;

                    MakeTreeInExpression(funcExpression);
                    MakeTreeInExpression(expression);
                    return;
                }

            }

            if (maxPriorityOperationIndex < 0
                || maxPriorityOperationIndex >= expression.SubExpressions.Count - 1)
                return;

            Expression operExpression = expression.SubExpressions[maxPriorityOperationIndex];
            Expression operNoralizedExpression = new Expression(runtimeControl);
            operNoralizedExpression.AddExpression(operExpression);
            expression.SubExpressions[maxPriorityOperationIndex] = operNoralizedExpression;
            
            ArgumentsExpression operArgumentsExpression = new ArgumentsExpression(runtimeControl);
            operExpression.AddExpression(operArgumentsExpression);

            operArgumentsExpression.AddExpression(expression.SubExpressions[maxPriorityOperationIndex + 1]);
            expression.SubExpressions.RemoveAt(maxPriorityOperationIndex + 1);

            if ((operExpression as OperationExpression).Function.ArgsCount == 2)
            {
                operArgumentsExpression.SubExpressions.Insert(0,expression.SubExpressions[maxPriorityOperationIndex - 1]);
                expression.SubExpressions.RemoveAt(maxPriorityOperationIndex - 1);
            }

            MakeTreeInExpression(operNoralizedExpression);
            MakeTreeInExpression(expression);
        }
    }
}
