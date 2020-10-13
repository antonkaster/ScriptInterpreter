using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class NotEqualLogicOperationTest : LanguageTestBase
    {

        [TestMethod]
        public void NumNotEqualNumTest()
        {
            SObject result = ResetParseAndGo("1 != 2");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void BoolNotEqualBoolTest()
        {
            SObject result = ResetParseAndGo("true != false");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void BoolNotEqualBool2Test()
        {
            SObject result = ResetParseAndGo("false != false");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void BoolNotEqualTextTest()
        {
            SObject result = ResetParseAndGo("true != \"asd123\"");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void TextNotEqualBoolTest()
        {
            SObject result = ResetParseAndGo("\"false\" != false");
            Assert.AreEqual(new SObject(true), result);

        }

        [TestMethod]
        public void TextNotEqualTextTest()
        {
            SObject result = ResetParseAndGo("\"asd123\" != \"asd123\"");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void TextNotEqualText2Test()
        {
            SObject result = ResetParseAndGo("\"asd123\" != \"qwe \"");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void TextNotEqualNumTest()
        {
            SObject result = ResetParseAndGo("\"qwe\" != 123");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void NumNotEqualNum2Test()
        {
            SObject result = ResetParseAndGo("3 !=3");
            Assert.AreEqual(new SObject(false), result);
        }
    }
}
