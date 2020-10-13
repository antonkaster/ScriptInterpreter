using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.FunctionsTests
{
    [TestClass]
    public class StopFunctionTest : LanguageTestBase
    {
        [TestMethod]
        public void StopTest()
        {
            SObject result = ResetParseAndGo("a=1; a; stop; a=2; a;");
            Assert.AreNotEqual(new SObject(2), result);
        }
    }
}
