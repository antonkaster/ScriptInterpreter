using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.FunctionsTests
{
    [TestClass]
    public class PrintLineFunctionTest : LanguageTestBase
    {
        [TestMethod]
        public void PrintLineNumTest()
        {
            SObject result = ResetParseAndGo("printline(33)");
            Assert.AreEqual("33\r\n", ScriptConsoleOut);
        }
        
        [TestMethod]
        public void PrintLineTextTest()
        {
            SObject result = ResetParseAndGo("printline(\"Test 123!\")");
            Assert.AreEqual("Test 123!\r\n", ScriptConsoleOut);
        }
    }
}
