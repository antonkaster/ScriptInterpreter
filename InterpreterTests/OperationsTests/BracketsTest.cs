using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class BracketsTest : LanguageTestBase
    {
        [TestMethod]
        public void Brackets1Test()
        {
            SObject result = ResetParseAndGo("(1 + 2) * 3");
            Assert.AreEqual(new SObject(9), result);
        }
        
        [TestMethod]
        public void Brackets2Test()
        {
            SObject result = ResetParseAndGo("1 + (2 * 3)");
            Assert.AreEqual(new SObject(7), result);
        }
        
        [TestMethod]
        public void MultiBrackets1Test()
        {
            SObject result = ResetParseAndGo("(2 * (3+1)) * 3");
            Assert.AreEqual(new SObject(24), result);
        }
        
        [TestMethod]
        public void MultiBrackets2Test()
        {
            SObject result = ResetParseAndGo("(2 * (3+1)) * (3 + 1)");
            Assert.AreEqual(new SObject(32), result);
        }
    }
}
