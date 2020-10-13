using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class OrLogicOperationTest : LanguageTestBase
    {

        [TestMethod]
        public void TrueOrTrueTest()
        {            
            SObject result = ResetParseAndGo("true || true");
            Assert.AreEqual(new SObject(true), result);            
        }

        [TestMethod]
        public void TrueOrFalseTest()
        {
            SObject result = ResetParseAndGo("true || false");
            Assert.AreEqual(new SObject(true), result);
        }
    
        [TestMethod]
        public void FalseOrTrueTest()
        {
            SObject result = ResetParseAndGo("false || true");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void FalseOrFalseTest()
        {
            SObject result = ResetParseAndGo("false || false");
            Assert.AreEqual(new SObject(false), result);
        }

    }
}
