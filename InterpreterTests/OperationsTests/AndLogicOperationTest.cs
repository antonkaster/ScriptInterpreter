using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class AndLogicOperationTest : LanguageTestBase
    {
        [TestMethod]
        public void TrueAndTrueTest()
        {
            SObject result = ResetParseAndGo("true && true");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void FalseAndFalseTest()
        {
            SObject result = ResetParseAndGo("false && false");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void FalseAndTrueTest()
        {
            SObject result = ResetParseAndGo("false && true");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void TrueAndFalseTest()
        {
            SObject result = ResetParseAndGo("true && false");
            Assert.AreEqual(new SObject(false), result);
        }
    }
}
