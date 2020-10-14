# Интерпретатор скриптового языка

## InterpreterLib
Основная библиотека интерпретатора

## InterpreterTests
Тесты функция языка

## ScriptExamples
Примеры сценариев

## ConsoleInterpreter
Консольный интерпретатор сценариев

## ScriptIDE
Простая IDE для отладки сценариев

## Простой пример использования

```C#
string scriptText = "Print((2 + 3) * 2)";
ScriptBase scriptBase = new ScriptBase();
scriptBase.ConsoleOut += (text) => Console.Write(text);
scriptBase.ErrorOut += (token, text) => Console.WriteLine("\r\nError: " + text);

scriptBase
	.Parser
	.Parse(scriptText)
	.Go();
```
