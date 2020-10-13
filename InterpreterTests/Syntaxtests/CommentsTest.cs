using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.Syntaxtests
{
    [TestClass]
    public class CommentsTest : LanguageTestBase
    {
        [TestMethod]
        public void LineComment1Test()
        {
            SObject result = ResetParseAndGo("a=1;\r\n//comment 1!\r\na=a+1;a");
            Assert.AreEqual(new SObject(2), result);
        }

        [TestMethod]
        public void LineComment2Test()
        {
            SObject result = ResetParseAndGo("a=1;//comment 1!\r\na=a+1;a");
            Assert.AreEqual(new SObject(2), result);
        }
        
        [TestMethod]
        public void LineComment3Test()
        {
            SObject result = ResetParseAndGo("a=1;\r\n//comment 1!\r\na=a+1;// qwe 1\r\na");
            Assert.AreEqual(new SObject(2), result);
        }

        [TestMethod]
        public void LineComment4Test()
        {
            SObject result = ResetParseAndGo("a=1;//comment 1!\r\na=a+1;a///comment 2");
            Assert.AreEqual(new SObject(2), result);
        }
    }
}
