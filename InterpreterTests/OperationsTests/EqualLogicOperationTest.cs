using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class EqualLogicOperationTest : LanguageTestBase
    {

        [TestMethod]
        public void NumEqualNumTest()
        {
            SObject result = ResetParseAndGo("1 == 2");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void BoolEqualBoolTest()
        {
            SObject result = ResetParseAndGo("true == false");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void BoolEqualBool2Test()
        {
            SObject result = ResetParseAndGo("false == false");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void BoolEqualTextTest()
        {
            SObject result = ResetParseAndGo("true == \"asd123\"");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void TextEqualBoolTest()
        {
            SObject result = ResetParseAndGo("\"false\" == false");
            Assert.AreEqual(new SObject(false), result);

        }

        [TestMethod]
        public void TextEqualTextTest()
        {
            SObject result = ResetParseAndGo("\"asd123\" == \"asd123\"");
            Assert.AreEqual(new SObject(true), result);
        }

        [TestMethod]
        public void TextEqualText2Test()
        {
            SObject result = ResetParseAndGo("\"asd123\" == \"qwe \"");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void TextEqualNumTest()
        {
            SObject result = ResetParseAndGo("\"qwe\" == 123");
            Assert.AreEqual(new SObject(false), result);
        }

        [TestMethod]
        public void NumEqualNum2Test()
        {
            SObject result = ResetParseAndGo("3 ==3");
            Assert.AreEqual(new SObject(true), result);
        }
    }
}
