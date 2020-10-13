using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.BasicTypesTests
{

    [TestClass]
    public class ArrayTest : LanguageTestBase
    {
        [TestMethod]
        public void BasicArrayTest()
        {
            SObject result = ResetParseAndGo("a[0]=1;a[0];");
            Assert.AreEqual(new SObject(1), result);
        }
        
        [TestMethod]
        public void SummArrayTest()
        {
            SObject result = ResetParseAndGo("a[0]=1; a[1]=a[0]+a[0]; a[1];");
            Assert.AreEqual(new SObject(2), result);
        }
        
        [TestMethod]
        public void TextArrayTest()
        {
            SObject result = ResetParseAndGo("a[\"test\"]=\"text\"; a[\"test\"];");
            Assert.AreEqual(new SObject("text"), result);
        }        

        [TestMethod]
        public void EmptyArrayTest()
        {
            SObject result = ResetParseAndGo("a[0]=1; a[1];");
            Assert.AreEqual(new SObject(), result);
        }

        [TestMethod]
        public void ArrayArrayTest()
        {
            SObject result = ResetParseAndGo("a[0]=1; a[a[0]] = \"text\"; a[1];");
            Assert.AreEqual(new SObject("text"), result);
        }

        [TestMethod]
        public void SummArrayArrayTest()
        {
            SObject result = ResetParseAndGo("a[0]=1; a[a[0]+2] = \"text\"; a[3];");
            Assert.AreEqual(new SObject("text"), result);
        }
    }
}
