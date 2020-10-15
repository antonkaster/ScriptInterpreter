using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Highlighting.Xshd;
using InterpreterLib;
using InterpreterLib.Expressions;
using InterpreterLib.InterpreterModules;
using InterpreterLib.Tokens;
using LangGUI.Controls;
using LangGUI.Helpers;
using LangGUI.Models;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Utilities;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;

namespace LangGUI.ViewModels
{
    public class MainViewModel : ObservableObject
    {

        public bool EnableDebugOutput
        {
            get => langBase.Logger.LogOptions.EnableDebug;
            set
            {
                langBase.Logger.LogOptions.EnableDebug = value;
                NotifyPropertyChanged();
            }
        }

        private string filename;
        public string FileName
        {
            get => filename;
            private set
            {
                filename = value;
                NotifyPropertyChanged();
            }
        }

        private IHighlightingDefinition codeHighlightingScheme;
        public IHighlightingDefinition CodeHighlightingScheme
        {
            get => codeHighlightingScheme;
            private set
            {
                codeHighlightingScheme = value;
                NotifyPropertyChanged();
            }
        }

        private string code = "";
        public string CodeText
        {
            get => code;
            set
            {
                code = value;
                NotifyPropertyChanged();

                if (ColorizeProps?.End != 0)
                    ColorizeProps = new ColorizeProps();
            }
        }

        private string console;
        public string Console
        {
            get => console;
            private set
            {
                console = value;

                if (console.Length > maxConsoleLength)
                    console = console.Substring(console.Length - maxConsoleLength);

                NotifyPropertyChanged();
            }
        }

        private string debug;
        public string Debug
        {
            get => debug;
            private set
            {
                debug = value;

                if (debug.Length > maxConsoleLength)
                    debug = debug.Substring(debug.Length - maxConsoleLength);

                NotifyPropertyChanged();
            }
        }

        private bool isWorking = false;
        public bool IsWorking
        {
            get => isWorking;
            set
            {
                isWorking = value;
                NotifyPropertyChanged();
                if (!value)
                    IsStepByStep = false;
            }
        }

        private bool isStepByStep = false;
        public bool IsStepByStep
        {
            get => isStepByStep;
            set
            {
                isStepByStep = value;
                NotifyPropertyChanged();
            }
        }

        private bool isStepAvalaible = false;
        public bool IsStepAvalaible
        {
            get => isStepAvalaible;
            set
            {
                isStepAvalaible = value;
                NotifyPropertyChanged();
            }
        }

        IEnumerable<string> tokensList;
        public IEnumerable<string> TokensList
        {
            get => tokensList;
            private set
            {
                tokensList = value;
                NotifyPropertyChanged();
            }
        }

        IEnumerable<string> variablesList;
        public IEnumerable<string> VariablesList
        {
            get => variablesList;
            private set
            {
                variablesList = value;
                NotifyPropertyChanged();
            }
        }

        private ColorizeProps colorizeProps = new ColorizeProps();
        public ColorizeProps ColorizeProps
        {
            get => colorizeProps;
            set
            {
                colorizeProps = value;
                NotifyPropertyChanged();
            }
        }

        List<ExpressionsTreeModel> expressionTree = new List<ExpressionsTreeModel>();
        public List<ExpressionsTreeModel> ExpressionTree
        {
            get => expressionTree;
            private set
            {
                expressionTree = value;
                NotifyPropertyChanged();
            }
        }

        public DelegateCommand GoCommand => new DelegateCommand(() => StartScript(false));
        public DelegateCommand GoStepByStepCommand => new DelegateCommand(() => StartScript(true));

        public DelegateCommand DoStepCommand => new DelegateCommand(() =>
        {
            if (!IsStepAvalaible)
                return;
            IsStepAvalaible = false;
            currentInterpreter.DoStep();
        });

        public DelegateCommand StopCommand => new DelegateCommand(() =>
        {
            Task.Run(() =>
            {
                currentInterpreter.Stop();
            });
        });

        public DelegateCommand ParseCommand => new DelegateCommand(() =>
        {
            Task.Run(() =>
            {
                currentInterpreter = langBase.Parser.Parse(CodeText);
                UpdateParseInfo();
            });
        });

        public DelegateCommand SaveCommand => new DelegateCommand(() => SaveCodeToFile(FileName));
        public DelegateCommand SaveAsCommand => new DelegateCommand(() => 
        {
            try
            {
                SaveFileDialog saveFile = new SaveFileDialog();
                saveFile.ShowDialog();
                if (!string.IsNullOrEmpty(saveFile.FileName))
                    SaveCodeToFile(saveFile.FileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Saving error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });
        public DelegateCommand OpenCommand => new DelegateCommand(() => 
        {
            try
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.ShowDialog();
                if (!string.IsNullOrEmpty(openFile.FileName))
                    LoadCodeFromFile(openFile.FileName);
            }
            catch(Exception  ex)
            {
                MessageBox.Show(ex.Message, "Opening error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        });

        public DelegateCommand ReloadCommand => new DelegateCommand(() =>
        {
            if (File.Exists(FileName))
                LoadCodeFromFile(FileName);
        });

        public DelegateCommand ClearAllCommand => new DelegateCommand(() =>
        {
            Console = "";
            Debug = "";
        });


        private readonly int maxConsoleLength = 100000;
        private readonly ScriptBase langBase;

        private bool inError = false;

        private IScriptInterpreter currentInterpreter = null;

        public MainViewModel()
        {
            FileName = Path.GetFullPath("scripts\\testscript.txt");

            LoadCodeFromFile(FileName);

            langBase = new ScriptBase();
            langBase.Logger.LogOptions.EnableDebug = true;
            langBase.Logger.OnConsoleOut += (text) => Console += text;
            langBase.Logger.OnLogDebug += (text) => PrintToDebug(text);
            langBase.Logger.OnLogError += (text) => LangBase_ErrorOut(new Token(), text);
            langBase.Logger.OnLogTokenizedError += (token,text) => LangBase_ErrorOut(token, text);

            CodeHighlightingScheme = new HighlightGenerator(langBase).Make();
        }

        private void LangBase_BeforeStepExec(Token token)
        {
            ColorizeProps = new ColorizeProps(token.StartIndex, token.StartIndex + token.TokenStringLenght, Colors.Magenta, Colors.White);
            UpdateDebugInfo();
            IsStepAvalaible = true;
        }

        private void LangBase_StepExec(Token token)
        {
            UpdateDebugInfo();
        }

        private void StartScript(bool stepByStep)
        {
            if (IsWorking)
                return;

            IsWorking = true;
            inError = false;
            IsStepByStep = stepByStep;
            ColorizeProps = new ColorizeProps();
            Console += "\r\n";

            Task.Run(() =>
            {
                currentInterpreter = langBase.Parser.Parse(CodeText);
                currentInterpreter.RuntimeControl.StepExec += LangBase_StepExec;
                currentInterpreter.RuntimeControl.BeforeStepExec += LangBase_BeforeStepExec;
                currentInterpreter.ScriptFinished += (obj) => PrintToDebug($"Script result ({obj.Type}):\r\n{obj}");

                UpdateParseInfo();
                UpdateDebugInfo();

                Task.Run(() =>
                {
                    do
                    {
                        Thread.Sleep(500);
                        if (IsWorking != currentInterpreter.RuntimeControl.ScriptInProcess)
                            IsWorking = currentInterpreter.RuntimeControl.ScriptInProcess;
                    }
                    while (IsWorking);
                });

                currentInterpreter.Go(stepByStep);

                if (IsStepByStep && !inError)
                    ColorizeProps = new ColorizeProps();

                UpdateDebugInfo();
                IsWorking = false;

            });
        }

        public void UpdateDebugInfo()
        {
            VariablesList = currentInterpreter?.Vars.Variables.Select(t => $"[{t.Value.Type}] {t.Key} = {t.Value}");
        }

        private void UpdateParseInfo()
        {
            TokensList = currentInterpreter.GetTokens().Select(t => t.ToString());
            List<ExpressionsTreeModel> expressionsTreeModels = new List<ExpressionsTreeModel>();
            expressionsTreeModels.Add(new ExpressionsTreeModel(currentInterpreter.GetRootExpression()));
            ExpressionTree = expressionsTreeModels;
        }

        private void LangBase_ErrorOut(Token token, string text)
        {
            inError = true;
            ColorizeProps = new ColorizeProps(token.StartIndex, token.StartIndex + token.TokenStringLenght, Colors.Red, Colors.White);
            UpdateDebugInfo();
            PrintToDebug($"Error: \r\n{text}");
        }

        private void PrintToDebug(string text)
        {
            Debug += $"[{DateTime.Now:HH:dd:mm:ss.fff}] {text}\r\n";
        }

        private void LoadCodeFromFile(string file)
        {
            if (!File.Exists(file))
                throw new FileNotFoundException($"File '{file}' not found!");
            
            FileName = file;            
            CodeText = File.ReadAllText(FileName);
        }

        private void SaveCodeToFile(string file)
        {
            FileName = file;
            File.WriteAllText(file, CodeText);
        }        

    }
}
