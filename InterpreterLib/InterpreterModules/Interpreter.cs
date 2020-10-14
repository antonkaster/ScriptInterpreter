using InterpreterLib.Environment;
using InterpreterLib.Expressions;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterLib.InterpreterModules
{
    /// <summary>
    /// Интерпретатор сценария
    /// </summary>
    public class Interpreter : IScriptInterpreter
    {
        public event IScriptInterpreter.ScriptFinishedMethod ScriptFinished;
        public IVariablesHeap Vars { get => scriptEnvironment.Vars; }
        public IExternalRuntimeControl RuntimeControl { get => runtimeControl; }

        private readonly ScriptRuntimeControl runtimeControl;
        private readonly ScriptEnvironment scriptEnvironment;
        private readonly Expression rootExpression;
        private readonly List<Token> tokens;

        internal Interpreter(ScriptEnvironment scriptEnvironment, ScriptRuntimeControl runtimeControl, Expression rootExpression, List<Token> tokens)
        {
            this.scriptEnvironment = scriptEnvironment ?? throw new ArgumentNullException("Script environment can't be null!");
            this.runtimeControl = runtimeControl ?? throw new ArgumentNullException("Runtime control can't be null!");
            this.rootExpression = rootExpression ?? throw new ArgumentNullException("Root expression can't be null!");
            this.tokens = tokens ?? throw new ArgumentNullException("Tokens list can't be null!");
        }

        public SObject Go(bool stepByStep = false, bool resetEnvironment = false)
        {
            if (resetEnvironment)
                ResetEnvironment();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Restart();

            try
            {
                RuntimeControl.ScriptStart(stepByStep);

                SObject result = rootExpression.GetResult();

                stopwatch.Stop();

                RuntimeControl.ScriptStop();

                WriteDebugOut($"Finished in {stopwatch.Elapsed}");

                SendScriptResult(result);
                return result;
            }
            catch (ScriptStopException ex)
            {
                RuntimeControl.ScriptStop();
                WriteDebugOut($"Script stopped by reason '{ex.Reason}'");
                Debug.Print($"Script stopped by reason '{ex.Reason}'");
                if (ex.ReturnObject != null)
                    SendScriptResult(ex.ReturnObject);
                return ex.ReturnObject;

            }
            catch (ScriptFunctionException ex)
            {
                RuntimeControl.ScriptStop();
                WriteErrorOut(ex.Token, $"Script function error: {ex.Message}");
                Debug.Print($"Script function ({ex.Token}) error: {ex.Message}");
                return new SObject();
            }
            catch (ScriptRuntimeException ex)
            {
                RuntimeControl.ScriptStop();
                WriteErrorOut(ex.Token, $"Script runtime error: {ex.Message}");
                Debug.Print($"Script runtime ({ex.Token}) error: {ex.Message}");
                return new SObject();
            }
            catch (OperationCanceledException ex)
            {
                RuntimeControl.ScriptStop();
                WriteErrorOut(new Token(), $"Script canceled by unknown reason ({ex.Message})");
                Debug.Print($"Script canceled by unknown reason ({ex.Message})");
                return new SObject();
            }
            catch (Exception ex)
            {
                RuntimeControl.ScriptStop();
                WriteErrorOut(new Token(), $"Error: {ex.Message}");
                Debug.Print($"Error: {ex.Message}");
                return new SObject();
            }
        }

        public Task<SObject> GoAsync(bool stepByStep, bool resetEnvironment = true)
        {
            var task = new Task<SObject>(() => Go(stepByStep, resetEnvironment));
            task.Start();
            return task;
        }

        public void DoStep()
        {
            RuntimeControl.EnableNextStep();
        }

        public void Stop()
        {
            RuntimeControl.ScriptStop();
            WriteDebugOut($"Script aborted!");
        }

        public void ResetEnvironment()
        {
            scriptEnvironment.ResetEnvironment();
        }


        public Expression GetRootExpression()
        {
            if (rootExpression == null)
                return new Expression(runtimeControl);
            return rootExpression;
        }

        public List<Token> GetTokens()
        {
            if (tokens == null)
                return new List<Token>();
            return tokens;
        }


        private void WriteDebugOut(string text)
        {
            scriptEnvironment.Logger.LogDebug("Debug: " + text);
        }

        private void WriteErrorOut(Token token, string text)
        {
            WriteDebugOut(text);
            scriptEnvironment.Logger.LogDebug("Error: " + text);
        }

        private void SendScriptResult(SObject obj)
        {
            ScriptFinished?.Invoke(obj);
        }
    }
}
