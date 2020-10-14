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

## Примеры использования

Простое вычисление:
```C#
deciaml result = new ScriptBase()
	.Parser
	.Parse("(2+2)*2")
	.Go()
	.NumValue;
```

Привязка с выводом в консоль:
```C#
ScriptBase scriptBase = new ScriptBase();
scriptBase.ConsoleOut += (text) => Console.Write(text);
scriptBase.ErrorOut += (token, text) => Console.WriteLine("\r\nError: " + text);

scriptBase
	.Parser
	.Parse("Print((2 + 3) * 2)")
	.Go();
```

Загрузить и выполнить из файла:
```C#
ScriptBase scriptBase = new ScriptBase();
scriptBase.ConsoleOut += (text) => Console.Write(text);
scriptBase.ErrorOut += (token, text) => Console.WriteLine("\r\nError: " + text);

scriptBase
	.Parser
	.LoadFromFileAndParse(@".\scripts\test.txt")
	.Go()
```
