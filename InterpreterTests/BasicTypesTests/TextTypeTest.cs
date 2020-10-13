using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.BasicTypesTests
{
    [TestClass]
    public class TextTypeTest : LanguageTestBase
    {
        [TestMethod]
        public void TextTest()
        {
            SObject result = ResetParseAndGo("\"test 123: \"");
            Assert.AreEqual(new SObject("test 123: "), result);
        }
    }
}
