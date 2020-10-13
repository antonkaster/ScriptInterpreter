﻿using InterpreterLib.ScriptExceptions;
using InterpreterLib.Logs;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using InterpreterLib.ScriptObjects;

namespace InterpreterLib.Environment
{
    /// <summary>
    /// Управление выполнением сценария
    /// </summary>
    internal class ScriptRuntimeControl : IExternalRuntimeControl, IInternalRuntimeControl
    {
        public event IExternalRuntimeControl.StepExecMethod BeforeStepExec;
        public event IExternalRuntimeControl.StepExecMethod StepExec;

        public bool ScriptInProcess { get; private set; } = false;
        public bool StepByStepExecMode { get; private set; } = false;

        public CancellationToken CancellationToken { get => cts.Token; }

        private readonly IInterpreterLogger logger;
        private CancellationTokenSource cts = new CancellationTokenSource();
        private bool enableStepFlag = false;

        public ScriptRuntimeControl(IInterpreterLogger logger)
        {
            this.logger = logger ?? throw new ArgumentNullException("Logger can't be null!");
        }

        public void ScriptStart(bool stepByStepMode = false)
        {
            if (ScriptInProcess)
                return;

            cts = new CancellationTokenSource();

            enableStepFlag = false;
            StepByStepExecMode = stepByStepMode;
            ScriptInProcess = true;
        }

        public void ScriptStop()
        {
            if (!ScriptInProcess)
                return;

            EnableNextStep();
            cts.Cancel();
            ScriptInProcess = false;
        }

        public void EnableNextStep()
        {
            if (!ScriptInProcess)
                return;

            if (!StepByStepExecMode)
                return;

            enableStepFlag = true;
        }

        public void WaitGoNextStepOrExit(Token token)
        {
            if (StepByStepExecMode && token.TokenType != TokenType.Empty)
            {
                BeforeStepExec?.Invoke(token);
                while (!enableStepFlag)
                {
                    Thread.Sleep(100);
                }
                enableStepFlag = false;
                StepExec?.Invoke(token);
            }

            if (CancellationToken.IsCancellationRequested)
                throw new ScriptStopException(ScriptStopReason.ExternalCancellation);

            if (token.TokenType != TokenType.Empty)
                logger.LogDebug($"{token} excecute");

        }

    }
}