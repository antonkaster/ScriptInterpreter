using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.OperationsTests
{
    [TestClass]
    public class MultiOperationsTest : LanguageTestBase
    {
        [TestMethod]
        public void MultiOperation1Test()
        {
            SObject result = ResetParseAndGo("1 + 2 * 3");
            Assert.AreEqual(new SObject(7), result);
        }
        
        [TestMethod]
        public void MultiOperation2Test()
        {
            SObject result = ResetParseAndGo("1 + 2 * 3 +2");
            Assert.AreEqual(new SObject(9), result);
        }
                
        [TestMethod]
        public void MultiOperation3Test()
        {
            SObject result = ResetParseAndGo("2*7-3+2");
            Assert.AreEqual(new SObject(13), result);
        }
        
        [TestMethod]
        public void MultiOperation4Test()
        {
            SObject result = ResetParseAndGo("1 + 10 /2 *3");
            Assert.AreEqual(new SObject(16), result);
        }
        

    }
}
