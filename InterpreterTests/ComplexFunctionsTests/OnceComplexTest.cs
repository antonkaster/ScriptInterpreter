using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.ComplexFunctionsTests
{
    [TestClass]
    public class OnceComplexTest : LanguageTestBase
    {
        [TestMethod]
        public void Once1Test()
        {
            SObject result = ResetParseAndGo("once{ a = 2; } a = a + 1; a");
            Assert.AreEqual(new SObject(3), result);
        }
        
        [TestMethod]
        public void Once2Test()
        {
            SObject result = ResetParseAndGo("once{ a = 2; } a = a + 1; a");
            result = Go();
            Assert.AreEqual(new SObject(4), result);
        }

        [TestMethod]
        public void Once3Test()
        {
            SObject result = ResetParseAndGo("once{ a = 2; } a = a + 1; a");
            result = Go();
            result = Go();
            Assert.AreEqual(new SObject(5), result);
        }
    }
}
