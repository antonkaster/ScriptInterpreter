using InterpreterLib.Environment;
using InterpreterLib.ScriptExceptions;
using InterpreterLib.Functions;
using InterpreterLib.Functions.ComplexFunctions;
using InterpreterLib.Functions.Operations;
using InterpreterLib.ScriptObjects;
using InterpreterLib.LexerModules;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace InterpreterLib.InterpreterModules
{
    internal class FunctionsRepository : IFunctionsRepository
    {
        private readonly IFunctionEnvironment environment;

        private readonly Dictionary<string, FunctionStorage> operations = new Dictionary<string, FunctionStorage>();
        private readonly Dictionary<string, FunctionStorage> functions = new Dictionary<string, FunctionStorage>();        
        private readonly Dictionary<string, FunctionStorage> complexFunctions = new Dictionary<string, FunctionStorage>();

        public FunctionsRepository(IFunctionEnvironment functionEnvironment)
        {
            this.environment = functionEnvironment;
        }

        public bool IsNameAvalible(string name)
        {
            name = name.ToLower();
            
            if (LexerHelper.IsTokenSeparator(name))
                return false;
            if (operations.ContainsKey(name))
                return false;
            if (complexFunctions.ContainsKey(name))
                return false;
            if (functions.ContainsKey(name))
                return false;

            return true;
        }
        
        public IFunctionsRepository AddOperation<T>(string keyWord, params object[] args) where T: OperationFunctionBase
        {
            if (!IsNameAvalible(keyWord))
                throw new ScriptInitException($"Keyword '{keyWord}' already used!");
            operations[keyWord.ToLower()] = new FunctionStorage(typeof(T), args);
            return this;
        }

        public IFunctionsRepository AddFunction<T>(string keyWord, params object[] args) where T : FunctionBase
        {
            if (!LexerHelper.IsCorrectIdentifier(keyWord))
                throw new ScriptInitException($"Keyword '{keyWord}' is not correct identifier!");
            if (!IsNameAvalible(keyWord))
                throw new ScriptInitException($"Keyword '{keyWord}' already used!");
            functions[keyWord.ToLower()] = new FunctionStorage(typeof(T), args);
            return this;
        }

        public IFunctionsRepository AddComplexFunction<T>(string keyWord, params object[] args) where T : ComplexFunctionBase
        {
            if (!LexerHelper.IsCorrectIdentifier(keyWord))
                throw new ScriptInitException($"Keyword '{keyWord}' is not correct identifier!");
            if (!IsNameAvalible(keyWord))
                throw new ScriptInitException($"Keyword '{keyWord}' already used!");
            complexFunctions[keyWord.ToLower()] = new FunctionStorage(typeof(T), args);
            return this;
        }

        public OperationFunctionBase GetOperation(string name)
        {
            return GetFunc<OperationFunctionBase>(operations[name.ToLower()]);
        }

        public FunctionBase GetFunction(string name)
        {
            return GetFunc<FunctionBase>(functions[name.ToLower()]);
        }

        public ComplexFunctionBase GetComplexFunction(string name)
        {
            return GetFunc<ComplexFunctionBase>(complexFunctions[name.ToLower()]);
        }

        private T GetFunc<T>(FunctionStorage storage)
        {
            object[] args = new object[] { environment }
                .Concat(storage.Args)
                .ToArray();
            return (T)Activator.CreateInstance(storage.Type, args);
        }

        public string[] GetOperationsList()
        {
            return operations.Keys.ToArray();
        }
        public string[] GetFunctionsList()
        {
            return functions.Keys.ToArray();
        }
        public string[] GetComplexFunctionsList()
        {
            return complexFunctions.Keys.ToArray();
        }


    }
}
