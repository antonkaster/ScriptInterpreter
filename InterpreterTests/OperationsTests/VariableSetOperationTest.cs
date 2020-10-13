using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    class VariableSetOperationTest : LanguageTestBase
    {
        [TestMethod]
        public void NumericSetTest()
        {            
            SObject result = ResetParseAndGo("a = 12");
            Assert.AreEqual(new SObject(12), result);
        }

        [TestMethod]
        public void TextSetTest()
        {            
            SObject result = ResetParseAndGo("a = \"qwe! asdA\"");
            Assert.AreEqual(new SObject("qwe! asdA"), result);
        }
    }
}
