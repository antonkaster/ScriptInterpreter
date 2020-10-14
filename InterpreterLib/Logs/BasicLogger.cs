using InterpreterLib.ScriptObjects;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterLib.Logs
{
    public class BasicLogger : ILogger
    {
        public LoggerOptions LoggerOptions { get; private set; }


        private readonly Action<string> consoleOut;
        private readonly Action<string> logDebug;
        private readonly Action<string> logWarning;
        private readonly Action<Token, string> logTokenizedError;

        public BasicLogger(Action<string> consoleOut, Action<string> logDebug, Action<string> logWarning, Action<Token,string> logError)
        {
            this.consoleOut = consoleOut ?? throw new ArgumentNullException("ConsoleOut Action can't be null!");
            this.logDebug = logDebug ?? throw new ArgumentNullException("LogDebug Action can't be null!");
            this.logTokenizedError = logError ?? throw new ArgumentNullException("LogError Action can't be null!");
            this.logWarning = logWarning ?? throw new ArgumentNullException("LogWarning Action can't be null!");
            LoggerOptions = new LoggerOptions();
        }

        public void ConsoleOut(string text)
        {
            if(LoggerOptions.EnableConsoleOut)
                consoleOut.Invoke(text);
        }

        public void LogDebug(string text)
        {
            if(LoggerOptions.EnableDebug)
                logDebug.Invoke(text);
        }

        public void LogWarning(string text)
        {
            if(LoggerOptions.EnableWarning)
                logWarning.Invoke(text);
        }

        public void LogTokenizedError(Token token, string text)
        {
            if(LoggerOptions.EnableError)
                logTokenizedError.Invoke(token, text);
        }

        public void LogError(string text)
        {
            if(LoggerOptions.EnableError)
                logTokenizedError.Invoke(new Token(), text);
        }

    }

}
