using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.ComplexFunctionsTests
{
    [TestClass]
    public class IfComplexTests : LanguageTestBase
    {

        [TestMethod]
        public void IfTrueTest()
        {
            SObject result = ResetParseAndGo("a = 1; if(true) { a = 2; }; a;");
            Assert.AreEqual(new SObject(2), result);
        }

        [TestMethod]
        public void IfFalseTest()
        {
            SObject result = ResetParseAndGo("a = 1; if(false) { a = 2; }; a;");
            Assert.AreEqual(new SObject(1), result);
        }

        [TestMethod]
        public void IfTextEqualTest()
        {
            SObject result = ResetParseAndGo("a = 1; text = \"asd!\" if(\"asd!\" == text) { a = 2; }; a;");
            Assert.AreEqual(new SObject(2), result);
        }
        
        [TestMethod]
        public void IfTextEqual2Test()
        {
            SObject result = ResetParseAndGo("a = 1; text = \"asd!\" if(\"qwe1\" == text) { a = 2; }; a;");
            Assert.AreEqual(new SObject(1), result);
        }
        
        [TestMethod]
        public void IfNumEqualTest()
        {
            SObject result = ResetParseAndGo("a = 1; num = 123; if(num == 123) { a = 2; }; a;");
            Assert.AreEqual(new SObject(2), result);
        }
        
        [TestMethod]
        public void IfNumEqual2Test()
        {
            SObject result = ResetParseAndGo("a = 1; num = 123; if(num == 456) { a = 2; }; a;");
            Assert.AreEqual(new SObject(1), result);
        }

        [TestMethod]
        public void DoubleIfNumEqualTest()
        {
            SObject result = ResetParseAndGo("a = 1; if(a == 1) { a = 2; }; if(a==2) { a = 3; } a;");
            Assert.AreEqual(new SObject(3), result);
        }


    }
}
