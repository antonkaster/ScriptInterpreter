using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.FunctionsTests
{
    [TestClass]
    public class FalseFunctionTest : LanguageTestBase
    {
        [TestMethod]
        public void FalseTest()
        {
            SObject result = ResetParseAndGo("false");
            Assert.AreEqual(new SObject(false), result);
        }
    }
}
