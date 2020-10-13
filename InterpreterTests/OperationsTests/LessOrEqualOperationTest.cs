using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class LessOrEqualOperationTest : LanguageTestBase
    {
        [TestMethod]
        public void NumLessNumTest()
        {
            SObject result = ResetParseAndGo("1 <= 2");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void NumLessNum2Test()
        {
            SObject result = ResetParseAndGo("2 <= 1");
            Assert.AreEqual(new SObject(false), result);
        }
        
        [TestMethod]
        public void NumLessNum3Test()
        {
            SObject result = ResetParseAndGo("3 <= 3");
            Assert.AreEqual(new SObject(true), result);
        }
    }
}
