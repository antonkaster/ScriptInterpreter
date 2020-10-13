using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class MultipyOperationTest : LanguageTestBase
    {

        [TestMethod]
        public void MultiplyOperationTest()
        {
            SObject result = ResetParseAndGo("2*3");
            Assert.AreEqual(new SObject(6), result);
        }

        [TestMethod]
        public void MultiplyTextOperationTest()
        {
            SObject result = ResetParseAndGo("\"qw1!\"*3");
            Assert.AreEqual(new SObject("qw1!qw1!qw1!"), result);
        }

    }
}
