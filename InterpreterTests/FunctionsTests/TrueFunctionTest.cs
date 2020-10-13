using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.FunctionsTests
{
    [TestClass]
    public class TrueFunctionTest : LanguageTestBase
    {
        [TestMethod]
        public void TrueTest()
        {
            SObject result = ResetParseAndGo("true");
            Assert.AreEqual(new SObject(true), result);
        }
    }
}
