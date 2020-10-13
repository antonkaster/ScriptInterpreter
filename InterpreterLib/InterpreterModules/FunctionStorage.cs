using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterLib.InterpreterModules
{
    internal struct FunctionStorage
    {
        public Type Type;
        public object[] Args;

        public FunctionStorage(Type type, object[] args)
        {
            Type = type;
            Args = args;
        }
    }
}
