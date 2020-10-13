using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class NotLogicOperationTest : LanguageTestBase
    {

        [TestMethod]
        public void NotTrueTest()
        {            
            SObject result = ResetParseAndGo("!true");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void NotFalseTest()
        {
            SObject result = ResetParseAndGo("!false");
            Assert.AreEqual(new SObject(true), result);
        }
    }
}
