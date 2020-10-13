using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.BasicTypesTests
{
    [TestClass]
    public class BoolTypeTest : LanguageTestBase
    {
        [TestMethod]
        public void TrueTest()
        {
            SObject result = ResetParseAndGo("true");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void FalseTest()
        {
            SObject result = ResetParseAndGo("false");
            Assert.AreEqual(new SObject(false), result);
        }
    }
}
