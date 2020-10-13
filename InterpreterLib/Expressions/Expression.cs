using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.Extensions;
using InterpreterLib.Functions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Logs;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace InterpreterLib.Expressions
{
    /// <summary>
    /// Базовый тип выражения дерева исполнения
    /// </summary>
    public class Expression
    {
        /// <summary>
        /// Дочерние ветви выражения
        /// </summary>
        public List<Expression> SubExpressions { get; private set; } = new List<Expression>();

        /// <summary>
        /// Ключевой токен выражения
        /// </summary>
        public Token Token { get; private set; } = new Token();

        protected readonly IInternalRuntimeControl InternalControl;


        public Expression(IInternalRuntimeControl internalControl)
        {
            this.InternalControl = internalControl ?? throw new ArgumentNullException("internalControl can't be null!");
        }

        public Expression(IInternalRuntimeControl internalControl, Token token) 
            : this(internalControl)
        {            
            this.Token = token ?? throw new ArgumentNullException("Token can'tbe null!");
        }

        public Expression(IInternalRuntimeControl internalControl, List<Expression>  expressions) 
            : this(internalControl)
        {
            SubExpressions = expressions ?? throw new ArgumentNullException("SubExpressions can'tbe null!");
        }

        /// <summary>
        /// Добавить дочерние выражение
        /// </summary>
        /// <param name="expression"></param>
        public void AddExpression(Expression expression)
        {
            expression.ThrowIfNull("Expression can't be null");
            SubExpressions.Add(expression);
        }

        /// <summary>
        /// Добавить дочерние выражения из перечисления
        /// </summary>
        /// <param name="expressions"></param>
        public void AddExpressions(IEnumerable<Expression> expressions)
        {
            expressions.ThrowIfNull("Expression can't be null");
            SubExpressions.AddRange(expressions);
        }

        /// <summary>
        /// Выполнить выражение и получить результат
        /// </summary>
        /// <returns></returns>
        public SObject GetResult()
        {
            InternalControl.WaitGoNextStepOrExit(Token);

            try
            {
                SObject result = DoFunction();
                return result;
            }
            catch(ScriptStopException)
            {
                throw;
            }
            catch(ScriptFunctionException)
            {
                throw;
            }
            catch(ScriptRuntimeException)
            {
                throw;
            }
            catch(OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ScriptFunctionException(Token, ex.Message, ex);
            }

        }

        /// <summary>
        /// Метод выполнения полезной функции выражения
        /// </summary>
        /// <returns></returns>
        protected virtual SObject DoFunction()
        {
            bool complexExpressionCompleted = false;
            SObject tempValue = new SObject();

            foreach (Expression expression in SubExpressions)
            {
                if (expression is ComplexFunctionExpression complexExpr)
                {
                    if (complexExpr.Function.IsFirstComplexFunction || !complexExpressionCompleted)
                    {
                        SObject functResult = expression.GetResult();
                        complexExpressionCompleted = functResult.BoolValue;
                    }
                }
                else
                {
                    complexExpressionCompleted = false;
                    tempValue = expression.GetResult();
                }
            }
            return tempValue;
        }

        public override string ToString()
        {
            if(Token.TokenType == TokenType.Empty)
                return $"SubExpressions: {SubExpressions.Count}";
            else
                return $"{Token} SubExpressions: {SubExpressions.Count}";
        }
    }
}
