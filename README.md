# Интерпретатор скриптового языка

## [ConsoleInterpreter](https://github.com/antonkaster/ScriptInterpreter/tree/master/ConsoleInterpreter)
Консольный интерпретатор сценариев

## [InterpreterLib](https://github.com/antonkaster/ScriptInterpreter/tree/master/InterpreterLib)
Основная библиотека интерпретатора

## [InterpreterTests](https://github.com/antonkaster/ScriptInterpreter/tree/master/InterpreterTests)
Тесты функций языка

## [ScriptExamples](https://github.com/antonkaster/ScriptInterpreter/tree/master/ScriptExamples)
Примеры сценариев

## [ScriptIDE](https://github.com/antonkaster/ScriptInterpreter/tree/master/ScriptIDE)
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
