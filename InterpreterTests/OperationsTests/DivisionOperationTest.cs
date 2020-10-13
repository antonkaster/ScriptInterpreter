using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class DivisionOperationTest : LanguageTestBase
    {
        [TestMethod]
        public void NumericDivisionTest()
        {            
            SObject result = ResetParseAndGo("10/2");
            Assert.AreEqual(new SObject(5), result);
        }
    }
}
