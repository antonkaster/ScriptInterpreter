using InterpreterLib;
using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class MinusOperationTest : LanguageTestBase
    {

        [TestMethod]
        public void NumericMinusTest()
        {
            SObject result = ResetParseAndGo("2-1");
            Assert.AreEqual(new SObject(1), result);
        }

    }
}
