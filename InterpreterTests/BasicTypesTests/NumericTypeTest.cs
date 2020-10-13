using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.BasicTypesTests
{
    [TestClass]
    public class NumericTypeTest : LanguageTestBase
    {
        [TestMethod]
        public void IntTest()
        {
            SObject result = ResetParseAndGo("123");
            Assert.AreEqual(new SObject(123), result);
        }
        
        [TestMethod]
        public void DoubleTest()
        {
            SObject result = ResetParseAndGo("1.23");
            Assert.AreEqual(new SObject(1.23M), result);
        }
    }
}
