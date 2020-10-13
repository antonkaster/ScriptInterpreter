using InterpreterLib.InterpreterModules;
using InterpreterLib.ScriptObjects;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.ScriptObjects
{
    public class VariablesHeap : IVariablesHeap
    {
        public VariablesHeap()
        {
        }

        public SObject this [string varName]
        {
            get
            {
                string var = varName.ToLower();
                if (!Variables.ContainsKey(var))
                    Variables[var] = new SObject();

                return Variables[var];
            }
            set
            {
                this[varName].SetValue(value);
            }
        }

        public Dictionary<string, SObject> Variables { get; private set; } = new Dictionary<string, SObject>();

        public bool Contains(string varName)
        {
            return Variables.ContainsKey(varName.ToLower());
        }

        public void Clear()
        {
            Variables.Clear();
        }
    }
}
