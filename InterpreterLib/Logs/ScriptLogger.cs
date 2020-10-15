using InterpreterLib.ScriptObjects;
using InterpreterLib.Tokens;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace InterpreterLib.Logs
{
    internal class ScriptLogger : IInterpreterLoggerWriter, IScriptLoggerWriter, ILoggerReader
    {
        public event ILoggerReader.LogMethod OnConsoleOut;
        public event ILoggerReader.LogMethod OnLogDebug;
        public event ILoggerReader.LogMethod OnLogWarning;
        public event ILoggerReader.LogMethod OnLogError;
        public event ILoggerReader.LogTokenizedMethod OnLogTokenizedError;

        public LoggerOptions LogOptions { get; private set; }


        public ScriptLogger()
        {
            LogOptions = new LoggerOptions();
        }

        public void ConsoleOut(string text)
        {
            if(LogOptions.EnableConsoleOut)
                OnConsoleOut?.Invoke(text);
        }

        public void Debug(string text)
        {
            if(LogOptions.EnableDebug)
                OnLogDebug?.Invoke(text);
        }

        public void Warning(string text)
        {
            if(LogOptions.EnableWarning)
                OnLogWarning?.Invoke(text);
        }

        public void Error(Token token, string text)
        {
            WriteError(token, text);
        }

        public void Error(string text)
        {
            WriteError(null, text);
        }

        private void WriteError(Token token, string text)
        {
            Debug($"Error ({token}): {text}");
            if (LogOptions.EnableError)
            {
                if (token == null)
                    OnLogError?.Invoke(text);
                else
                    OnLogTokenizedError?.Invoke(token, text);
            }
        }
    }

}
