using InterpreterLib.ScriptObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.ScriptObjects
{
    public interface IVariablesHeap
    {
        public SObject this[string varName] { get;set; }
        public Dictionary<string, SObject> Variables { get; }
        public bool Contains(string varName);
    }
}
