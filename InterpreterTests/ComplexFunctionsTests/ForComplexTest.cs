using InterpreterLib.ScriptObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace InterpreterTests.ComplexFunctionsTests
{
    [TestClass]
    public class ForComplexTest : LanguageTestBase
    {
        [TestMethod]
        public void CounterForTest()
        {
            SObject result = ResetParseAndGo("a=1; for(i=0;i<=5;i=i+1){ a=a+1; }; a;");
            Assert.AreEqual(new SObject(7), result);
        }
        
        [TestMethod]
        public void CounterDoubleForTest()
        {
            SObject result = ResetParseAndGo("a=1; for(i=0;i<=5;i=i+1){ a=a+1; }; for(i=0;i<3;i=i+1){ a=a+1; }; a;");
            Assert.AreEqual(new SObject(10), result);
        }

        [TestMethod]
        public void BoolForTest()
        {
            SObject result = ResetParseAndGo("a=1; for(i=true;i;i=!i){ a=a+1; }; a;");
            Assert.AreEqual(new SObject(2), result);
        }
    }
}
