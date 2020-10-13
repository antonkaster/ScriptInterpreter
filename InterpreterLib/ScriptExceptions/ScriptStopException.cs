using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace InterpreterLib.ScriptExceptions
{
    public class ScriptStopException : OperationCanceledException
    {
        public ScriptStopReason Reason { get; }
        public SObject ReturnObject { get; } = null;

        public ScriptStopException()
        {
        }

        public ScriptStopException(ScriptStopReason reason) : base()
        {
            Reason = reason;
        }

        public ScriptStopException(ScriptStopReason reason, SObject returnObject) : base()
        {
            ReturnObject = returnObject;
            Reason = reason;
        }

    }

    public enum ScriptStopReason
    {
        ExternalCancellation,
        InFunctionStop,
        Other
    }
}
