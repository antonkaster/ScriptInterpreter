using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.FunctionsTests
{
    [TestClass]
    public class PrintFunctionTest : LanguageTestBase
    {
        [TestMethod]
        public void PrintNumTest()
        {
            SObject result = ResetParseAndGo("print(33)");
            Assert.AreEqual("33", ScriptConsoleOut);
        }
        
        [TestMethod]
        public void PrintTextTest()
        {
            SObject result = ResetParseAndGo("print(\"Test 123!\")");
            Assert.AreEqual("Test 123!", ScriptConsoleOut);
        }
    }
}
