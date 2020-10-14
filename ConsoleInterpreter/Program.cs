using InterpreterLib;
using InterpreterLib.InterpreterModules;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace ConsoleLangInterpreter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.ResetColor();
            string filename = "testscript.txt";

            if (args.Length > 0)
                filename = args[0];

            try
            {
                ScriptBase langBase = new ScriptBase();
                langBase.ConsoleOut += (text) =>
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.Write(text);
                    Console.ResetColor();
                };
                langBase.ErrorOut += (token, text) =>
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\r\n" + text);
                    Console.ResetColor();
                };

                langBase
                    .Parser
                    .LoadFromFileAndParse(filename)
                    .Go();

            }
            catch(Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Error: {ex.Message}\r\nDetails:\r\n{ex.StackTrace}");
                Console.ResetColor();
            }

        }
    }
}
