using InterpreterLib;
using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class PlusOperationTest : LanguageTestBase
    {

        [TestMethod]
        public void NumericPlusTest()
        {
            SObject result = ResetParseAndGo("1+2");
            Assert.AreEqual(new SObject(3), result);
        }

        [TestMethod]
        public void TextPlusIntTest()
        {
            SObject result = ResetParseAndGo("\"a!\"+2");
            Assert.AreEqual(new SObject("a!2"), result);
        }

        [TestMethod]
        public void TextPlusTextTest()
        {
            SObject result = ResetParseAndGo("\"a!\"+\": qwe\"");
            Assert.AreEqual(new SObject("a!: qwe"), result);
        }
    }
}
